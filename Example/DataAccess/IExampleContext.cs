using Example.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.DataAccess
{
    public interface IExampleContext
    {
        IDbSet<Address> Addresses { get; set; }
        IDbSet<Contact> Contacts { get; set; }
    }
}
