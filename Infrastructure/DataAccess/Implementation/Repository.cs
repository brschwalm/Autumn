using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Autumn.Mvc.Infrastructure.DataAccess.Implementation
{
	/// <summary>
	/// Base repository class that implements all CRUD operations
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <typeparam name="TPrimaryKey"></typeparam>
	public class Repository<TEntity, TPrimaryKey> : ReadOnlyRepository<TEntity, TPrimaryKey>, IRepository<TEntity, TPrimaryKey> where TEntity : class
	{
		#region Constructors

		/// <summary>
		/// Constructor for the generic repository.
		/// </summary>
		/// <param name="database"></param>
		public Repository(IApplicationContext context)
			: base(context)
		{
		}

		#endregion

		#region IRepository<TEntity> Members

		/// <summary>
		/// Inserts a new entity
		/// </summary>
		/// <param name="entity">The entit to add</param>
		public virtual void Insert(TEntity entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			if(this.PrepareInsert(entity))
				this.DbSet.Add(entity);
		}

		/// <summary>
		/// Updates an existing entity
		/// </summary>
		/// <param name="entity">The entity to update</param>
		public virtual void Update(TEntity entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");

			if (this.PrepareUpdate(entity))
			{
				this.DbSet.Attach(entity);
				this.Context.Entry(entity).State = EntityState.Modified;
			}
		}

		/// <summary>
		/// Deletes an entity by its Id.  Accounts for concurrency issues.
		/// </summary>
		/// <param name="id">The id of the entity to delete.</param>
		public void Delete(TPrimaryKey id)
		{
			TEntity entityToDelete = this.GetById(id);
			this.Delete(entityToDelete);
		}

		/// <summary>
		/// Deletes an entity.  Accounts for concurrency issues.
		/// </summary>
		/// <param name="entity">The entity to delete</param>
		public virtual void Delete(TEntity entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");

			if (this.PrepareDelete(entity))
			{
				if (this.Context.Entry(entity).State == EntityState.Detached)
					this.DbSet.Attach(entity);

				this.DbSet.Remove(entity);
			}
		}

		/// <summary>
		/// Deletes one or more entities based on a filter expression
		/// </summary>
		/// <param name="query">The query that returns the entities to delete </param>
		public void Delete(Expression<Func<TEntity, bool>> query)
		{
			IEnumerable<TEntity> entities = this.DbSet.Where<TEntity>(query).AsEnumerable();
			foreach (TEntity entity in entities)
				this.Delete(entity);
		}

		#endregion

		#region Protected Methods

		protected virtual bool PrepareInsert(TEntity entity) { return true; }
		protected virtual bool PrepareUpdate(TEntity entity) { return true; }
		protected virtual bool PrepareDelete(TEntity entity) { return true; }

		#endregion
	}
}
