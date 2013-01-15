using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Autumn.Mvc.Infrastructure.DataAccess.Implementation
{
	/// <summary>
	/// Base Repository that implements read-only operations
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <typeparam name="TPrimaryKey"></typeparam>
	public class ReadOnlyRepository<TEntity, TPrimaryKey> : Disposable, IReadOnlyRepository<TEntity, TPrimaryKey> where TEntity : class
	{
		#region Constructors

		/// <summary>
		/// Constructor for the generic repository.
		/// </summary>
		/// <param name="databaseFactory">The factory to create the database context</param>
		public ReadOnlyRepository(IApplicationContext context)
		{
			this.Context = context;
			this.DbSet = this.Context.Set<TEntity>();
		}

		#endregion

		#region Properties

		/// <summary>
		/// The database context against which we are operating.
		/// </summary>
		protected internal IApplicationContext Context { get; private set; }

		/// <summary>
		/// The DbSet of the entities we are operating on.
		/// </summary>
		protected internal IDbSet<TEntity> DbSet { get; private set; }

		#endregion

		#region IRepository<TEntity> Members

		/// <summary>
		/// Gets a list of entities filtered, ordered and with the included properties.
		/// </summary>
		/// <param name="filter">A filter to apply to the entities</param>
		/// <param name="orderBy">An expression to order the entities</param>
		/// <param name="includeProperties">The comma-separated list of properties to eager-load.</param>
		/// <returns>The list of entities that match the filter criteria, ordered appropriately and with eager-loaded properties.</returns>
		public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
		{
			IQueryable<TEntity> query = this.InitializeQuery(filter); // this.DbSet;

			//Add the includes
			query = this.AddIncludes(query, includeProperties);

			//Optionally Order the items, and return them
			if (orderBy != null)
				return orderBy(query).ToList();
			else
				return query.ToList();
		}

		/// <summary>
		/// Initializes the Query and adds the filter to the db set.
		/// </summary>
		/// <param name="filter">The filter to apply, or null if there is no filter</param>
		/// <returns>An IQueryable with the filter applied</returns>
		protected virtual IQueryable<TEntity> InitializeQuery(Expression<Func<TEntity, bool>> filter)
		{
			IQueryable<TEntity> query = this.DbSet;

			//Apply the filter
			if (filter != null)
				query = query.Where(filter);

			return query;
		}

		/// <summary>
		/// Gets a specific entity by its id, eager-loading additional properties.
		/// </summary>
		/// <param name="id">The ID of the entity to get</param>
		/// <param name="includeProperties">The list of properties to eager-load</param>
		/// <returns>The entity</returns>
		public virtual TEntity GetById(TPrimaryKey id, string includeProperties = null)
		{
			return this.DbSet.Find(id);
		}

		/// <summary>
		/// Gets a list of LOCAL entities.  Local entities are ones that have been added but are not yet saved to the DB
		/// </summary>
		/// <returns>The list of entities that match the filter criteria, ordered appropriately and with eager-loaded properties.</returns>
		public virtual IEnumerable<TEntity> LocalEntities { get { return this.DbSet.Local; } }

		/// <summary>
		/// Gets all the entities as an IQueryable collection for running LINQ Queries against the underlying data set.
		/// </summary>
		public IQueryable<TEntity> All { get { return this.DbSet; } }

		#endregion

		/// <summary>
		/// Adds includes to a query based on a list of included property names.
		/// </summary>
		/// <param name="query">The query to add the includes to</param>
		/// <param name="includes">The list of properties to include</param>
		/// <returns>The query updated with the list of includes</returns>
		protected virtual IQueryable<TEntity> AddIncludes(IQueryable<TEntity> query, string includes)
		{
			IQueryable<TEntity> working = query;

			if (!string.IsNullOrWhiteSpace(includes))
			{
				//Add the includes
				foreach (var includeProp in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
					working = AddInclude(working, includeProp);
			}

			return working;
		}

		/// <summary>
		/// Adds a single include to a query
		/// </summary>
		/// <param name="query">The query to add the include to</param>
		/// <param name="include">The include to add</param>
		/// <returns>The query updated with the include</returns>
		protected virtual IQueryable<TEntity> AddInclude(IQueryable<TEntity> query, string include)
		{
			if (!string.IsNullOrWhiteSpace(include))
			{
				return query.Include(include);
			}

			return query;
		}

		#region IDisposable Members

		/// <summary>
		/// Clean up the database context if necessary
		/// </summary>
		protected override void DisposeCore()
		{
			if (this.Context != null)
				this.Context.Dispose();
		}

		#endregion
	}
}
