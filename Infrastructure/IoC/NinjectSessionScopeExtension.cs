using System.Web;
using Ninject.Activation;
using Ninject.Syntax;

namespace Autumn.Mvc.Infrastructure.IoC
{
	/// <summary>
	/// This class has an extension method to add an InSessionScope scope to the Ninject Kernal.  This will allow
	/// instances created through Ninject to be one-per-session for the ASP.NET app.
	/// </summary>
	public static class NinjectSessionScopeExtension
	{
		private const string _sessionKey = "NinjectSessionScopeSyncRoot";

		/// <summary>
		/// Defines a scope of Session, based on the HttpContext Session.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="parent"></param>
		public static void InSessionScope<T>(this IBindingInSyntax<T> parent)
		{
			parent.InScope(SessionScopeCallback);
		}

		private static object SessionScopeCallback(IContext context)
		{
			if (HttpContext.Current.Session[_sessionKey] == null)
			{
				HttpContext.Current.Session[_sessionKey] = new object();
			}

			return HttpContext.Current.Session[_sessionKey];
		}
	}
}
