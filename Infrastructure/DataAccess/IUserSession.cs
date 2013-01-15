using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autumn.Mvc.Infrastructure.DataAccess
{
	/// <summary>
	/// Interface that accesses information about the current user session.  Allows the DataAccess layer 
	/// to restrict information/access to the logged-in user only (through dependency injection).
	/// </summary>
	public interface IUserSession
	{
		bool IsAuthenticated { get; }
		Guid CurrentUserId { get; }
	}
}
