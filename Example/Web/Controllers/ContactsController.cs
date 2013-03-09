using Autumn.Mvc.Infrastructure.DataAccess;
using Example.Domain;
using Example.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Example.Web.Controllers
{
	/// <summary>
	/// Controller for Contacts
	/// </summary>
    public class ContactsController : AutumnController<IAutumnService<Contact>, Contact>
    {
		public ContactsController(IAutumnService<Contact> service, IUserSession session)
			: base(service, session)
        {
        }

        #region Properties

		protected override string ControllerName
		{
			get { return "Contacts"; }
		}

        #endregion

    }
}