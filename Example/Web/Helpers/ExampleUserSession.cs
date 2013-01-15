using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Example.Web.Helpers
{
    public class ExampleUserSession : IExampleUserSession
    {
        private Guid _currentUserId = Guid.Empty;

        /// <summary>
        /// Gets the user id for the current session
        /// </summary>
        public Guid CurrentUserId
        {
            get
            {
                if (_currentUserId == Guid.Empty)
                {
                    //FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
                    //if (fIdentity == null)
                    //{
                    //    return Guid.Empty;
                    //}

                    //_currentUserId = Guid.Parse(fIdentity.Ticket.UserData);

                    //hard-code the user id for our purposes
                    _currentUserId = Guid.Parse("EAFE8C58-DF4E-4390-A93E-2F8370408CDE");
					//_currentUserId = Guid.Parse("CE4FEFCB-EE0A-41F6-B304-4BCDDAC09A9B");
                }

                return _currentUserId;
            }
        }

        /// <summary>
        /// Gets whether or not the current user is Authenticated
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                return this.CurrentUserId != Guid.Empty;
            }
        }
    }
}