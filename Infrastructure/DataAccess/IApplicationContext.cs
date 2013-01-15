using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autumn.Mvc.Infrastructure.DataAccess
{
	/// <summary>
	/// Interface for the Context.  Used for mocking and unit testing, as well as injection
	/// </summary>
	public interface IApplicationContext
	{
		IDbSet<TEntity> Set<TEntity>() where TEntity : class;
		DbEntityEntry Entry(object entity);
		DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

		/// <summary>
		/// Commits unsaved changes
		/// </summary>
		/// <returns></returns>
		int Commit();

		/// <summary>
		/// Gets the entities that have been changed
		/// </summary>
		IEnumerable<DbEntityEntry> ChangedEntities { get; }

		/// <summary>
		/// Disposes the context
		/// </summary>
		void Dispose();
	}
}
