using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace Autumn.Mvc.Infrastructure.DataAccess
{
    /// <summary>
    /// base class implementation for the Application Context interface
    /// </summary>
    public abstract class ApplicationContext : DbContext, IApplicationContext
    {
        public ApplicationContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public virtual int Commit()
        {
            return base.SaveChanges();
        }

        public virtual IEnumerable<DbEntityEntry> ChangedEntities
        {
            get { return base.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged); }
        }
    }
}
