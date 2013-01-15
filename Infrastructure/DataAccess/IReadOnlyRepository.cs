using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Autumn.Mvc.Infrastructure.DataAccess
{
	/// <summary>
	/// A repository that only supports Read operations
	/// </summary>
	public interface IReadOnlyRepository<TEntity, TPrimaryKey> where TEntity : class
	{
		/// <summary>
		/// Gets a list of entities filtered, ordered and with the included properties.
		/// </summary>
		/// <param name="filter">A filter to apply to the entities</param>
		/// <param name="orderBy">An expression to order the entities</param>
		/// <param name="includeProperties">The comma-separated list of properties to eager-load.</param>
		/// <returns>The list of entities that match the filter criteria, ordered appropriately and with eager-loaded properties.</returns>
		IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, 
								 Func<IQueryable<TEntity>, 
								 IOrderedQueryable<TEntity>> orderBy = null,
								 string includeProperties = "");

		/// <summary>
		/// Gets a specific entity by its id, eager-loading additional properties.
		/// </summary>
		/// <param name="id">The ID of the entity to get</param>
		/// <param name="includeProperties">The list of properties to eager-load</param>
		/// <returns>The entity</returns>
		TEntity GetById(TPrimaryKey id, string includeProperties = null);

		/// <summary>
		/// Gets a list of LOCAL entities.  Local entities are ones that have been added but are not yet saved to the DB
		/// </summary>
		/// <returns>The list of local entities.</returns>
		IEnumerable<TEntity> LocalEntities { get; }

		/// <summary>
		/// Gets all entities for this repository
		/// </summary>
		IQueryable<TEntity> All { get; }

	}
}
