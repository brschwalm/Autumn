using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace Autumn.Mvc.Infrastructure.Authentication
{
    public interface IMembershipService
    {
        int MinPasswordLength { get; }

        bool ValidateUser(string userName, string password);
        string GetCanonicalUsername(string userName);
        
		MembershipUser CreateUser(string userName, string password, string email, string openId, out MembershipCreateStatus status);
		MembershipUser CreateUser(string userName, string password, string email, out MembershipCreateStatus status);
		
		bool ChangePassword(string userName, string oldPassword, string newPassword);
        MembershipUser GetUserById(string id);
		MembershipUser GetUser(string userName);
    }
}
