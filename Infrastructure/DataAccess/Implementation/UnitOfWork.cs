using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autumn.Mvc.Infrastructure.DataAccess.Implementation
{
	/// <summary>
	/// Unit of Work that allows for committing of changes across multiple repositories.
	/// </summary>
	public class UnitOfWork : IUnitOfWork
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="context"></param>
		public UnitOfWork(IApplicationContext context)
		{
			this.Context = context;
		}

		/// <summary>
		/// The Database Context for this unit of work
		/// </summary>
		protected IApplicationContext Context { get; private set; }

		#region IUnitOfWork Members

		/// <summary>
		/// Commits unsaved changes to the data context.
		/// </summary>
		public int Commit()
		{
			return this.Context.Commit();
		}

		/// <summary>
		/// Gets whether or not there are changes
		/// </summary>
		public bool HasChanges
		{
			get
			{
				return this.Context.ChangedEntities.Count() > 0;
			}
		}

		/// <summary>
		/// Gets the entities that have changed.
		/// </summary>
		public IEnumerable<DbEntityEntry> ChangedEntities
		{
			get
			{
				return this.Context.ChangedEntities;
			}
		}


		#endregion
	}
}
