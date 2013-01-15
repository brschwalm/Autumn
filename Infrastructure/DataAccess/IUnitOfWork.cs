using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autumn.Mvc.Infrastructure.DataAccess
{
	/// <summary>
	/// Encapsulates a unit of work so commits can be transactional and span multiple repositories
	/// </summary>
	public interface IUnitOfWork
	{
		/// <summary>
		/// Commits any changes to the database.
		/// </summary>
		int Commit();

		/// <summary>
		/// Determines whether or not there are any changes to commit
		/// </summary>
		bool HasChanges { get; }

		/// <summary>
		/// Gets the entities that have been changed
		/// </summary>
		IEnumerable<DbEntityEntry> ChangedEntities { get; }
	}
}
