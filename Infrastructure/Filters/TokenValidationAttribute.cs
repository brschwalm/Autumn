using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Web;

namespace Autumn.Mvc.Infrastructure.Filters
{
	public class TokenValidationAttribute : System.Web.Http.Filters.ActionFilterAttribute
	{
		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			string token;
			try
			{
				token = actionContext.Request.Headers.GetValues("Authorization-Token").First();
			}
			catch (Exception)
			{
				actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
				{
					Content = new StringContent("Missing Authorization-Token")
				};

				return;
			}

			try
			{
				//Get the user from the repository
				base.OnActionExecuting(actionContext);
			}
			catch (Exception)
			{
				actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized)
				{
					Content = new StringContent("Unauthorized User")
				};

				return;
			}
		}
	}
}
