using Anythink.Mvc.Infrastructure.DataAccess;
using Anythink.Mvc.Infrastructure.DataAccess.Implementation;
using Example.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.DataAccess
{
    public interface IContactRepository : IRepository<Contact, int>
    {
    }

    public class ContactRepository : AnythinkModelRepository<Contact>, IContactRepository
    {
        public ContactRepository(IApplicationContext context, IUserSession session)
            : base(context, session)
        {
        }
    }
}
