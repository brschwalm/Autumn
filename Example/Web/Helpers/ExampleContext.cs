using Autumn.Mvc.Infrastructure.DataAccess;
using Example.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Web.Helpers
{
    /// <summary>
    /// DataContext for the Example Application
    /// </summary>
    public class ExampleContext : ApplicationContext, IExampleContext
    {
        public ExampleContext()
            : base("DefaultConnection")
        { }

        public IDbSet<Address> Addresses { get; set; }
        public IDbSet<Contact> Contacts { get; set; }
    }
}
