using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web;

namespace Autumn.Mvc.Infrastructure.Authentication
{
    public class FormsAuthenticationService : IFormsAuthentication
    {
		/// <summary>
		/// Signs the user into the application using Forms Authentication
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="createPersistentCookie"></param>
		/// <param name="userId"></param>
        public void SignIn(string userName, bool createPersistentCookie, string userId, TimeSpan? duration)
        {
			if (duration == null)
				duration = TimeSpan.FromMinutes(30);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                1, //version
                userName, // user name
                DateTime.Now,             //creation
                DateTime.Now.Add(duration.Value), //Expiration
                createPersistentCookie, //Persistent
				userId); //since Classic logins don't have a "Friendly Name".  OpenID logins are handled in the AuthController.

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
        }

		/// <summary>
		/// Signs the user out from the application
		/// </summary>
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}
