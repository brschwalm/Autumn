using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autumn.Mvc.Infrastructure.DataAccess
{
	/// <summary>
	/// An interface for a generic repository to access entities in the system
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	public interface IRepository<TEntity, TPrimaryKey> : IReadOnlyRepository<TEntity, TPrimaryKey> where TEntity : class
	{
		/// <summary>
		/// Inserts a new entity
		/// </summary>
		/// <param name="entity">The entit to add</param>
		void Insert(TEntity entity);

		/// <summary>
		/// Updates an existing entity
		/// </summary>
		/// <param name="entity">The entity to update</param>
		void Update(TEntity entity);

		/// <summary>
		/// Deletes an entity by its Id.  Accounts for concurrency issues.
		/// </summary>
		/// <param name="id">The id of the entity to delete.</param>
		void Delete(TPrimaryKey id);

		/// <summary>
		/// Deletes an entity.  Accounts for concurrency issues.
		/// </summary>
		/// <param name="entity">The entity to delete</param>
		void Delete(TEntity entity);

	}
}
