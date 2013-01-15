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
    public interface IAddressRepository : IRepository<Address, int>
    {
    }

    public class AddressRepository : AnythinkModelRepository<Address>, IAddressRepository
    {
        public AddressRepository(IApplicationContext context, IUserSession session)
            : base(context, session)
        {
        }
    }
}
