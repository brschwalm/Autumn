using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Ninject.Syntax;
using Anythink.Mvc.Infrastructure.Authentication;

namespace Anythink.Mvc.Infrastructure.IoC
{
	public enum BindingScope
	{
		Request = 1,
		Sesson = 2,
	}

	public class NinjectDependencyResolver : IDependencyResolver
	{
		private IKernel _kernel;

		public NinjectDependencyResolver()
		{
			_kernel = new StandardKernel();
			this.AddBindings();
		}

		#region Properties

		/// <summary>
		/// Gets the Kernel for this dependency resolver.
		/// </summary>
		public IKernel Kernel
		{
			get { return _kernel; }
		}

		#endregion

		#region Methods

		/// <summary>
		/// Binds a type
		/// </summary>
		/// <typeparam name="T">The type to bind</typeparam>
		/// <returns>an IBindingToSyntax with the binding</returns>
		public IBindingToSyntax<T> Bind<T>()
		{
			return _kernel.Bind<T>();
		}

		//public void AddSelfBinding<TSource>(BindingScope scope)
		//{
		//	switch (scope)
		//	{
		//		case BindingScope.Request:
		//			this.Kernel.Bind<TSource>().ToSelf().InRequestScope();
		//			break;
		//		case BindingScope.Sesson:
		//			this.Kernel.Bind<TSource>().ToSelf().InSessionScope();
		//			break;
		//		default:
		//			break;
		//	}
		//}

		//public void AddBinding<TSource, TDestination>(BindingScope scope) where TDestination : TSource
		//{
		//	switch (scope)
		//	{
		//		case BindingScope.Request:
		//			this.Kernel.Bind<TSource>().To<TDestination>().InRequestScope();
		//			break;
		//		case BindingScope.Sesson:
		//			this.Kernel.Bind<TSource>().To<TDestination>().InSessionScope();
		//			break;
		//		default:
		//			break;
		//	}
		//}

		/// <summary>
		/// Adds the list of known bindings
		/// </summary>
		private void AddBindings()
		{
			//Infrastructure
			this.Kernel.Bind<IDependencyResolver>().ToConstant(this);

            ////Accounts
            this.Kernel.Bind<IFormsAuthentication>().To<FormsAuthenticationService>();
            this.Kernel.Bind<IMembershipService>().To<AccountMembershipService>();
		}

		#endregion

		#region IDependencyResolver Members

		/// <summary>
		/// Gets a specific service by its type
		/// </summary>
		/// <param name="serviceType">The type to get</param>
		/// <returns>The instance of the service type</returns>
		public object GetService(Type serviceType)
		{
			return _kernel.TryGet(serviceType);
		}

		/// <summary>
		/// Gets a list of services of a specific type.
		/// </summary>
		/// <param name="serviceType">The type to get</param>
		/// <returns>A list of services that are bound to the type</returns>
		public IEnumerable<object> GetServices(Type serviceType)
		{
			return _kernel.GetAll(serviceType);
		}

		#endregion
	}
}