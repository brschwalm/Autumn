using Autumn.Mvc.Infrastructure.DataAccess;
using Autumn.Mvc.Infrastructure.DataAccess.Implementation;
using Autumn.Mvc.Infrastructure.IoC;
using Example.Domain;
using Example.Services;
using Example.Web.Helpers;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Example.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : NinjectHttpApplication
    {
        protected override IKernel CreateKernel()
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);

            //Install our Ninject-based IDependencyResolver into the Web API Config
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectHttpDependencyResolver(kernel);

            return kernel;
        }

        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();
            Database.SetInitializer(new ExampleContextInitializer());

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected override void OnApplicationStopped()
        {
            base.OnApplicationStopped();
        }

        protected void RegisterServices(IKernel kernel)
        {
            //Bind the user session to itself
            kernel.Bind<ExampleUserSession>().ToSelf();
            //Then, bind the interface to the get from the kernel so we get a singleton
            kernel.Bind<IUserSession>().ToMethod(s => s.Kernel.Get<ExampleUserSession>());
            kernel.Bind<IExampleUserSession>().ToMethod(s => s.Kernel.Get<ExampleUserSession>());

            kernel.Bind<IApplicationContext>().To<ExampleContext>().InRequestScope();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();

            //Repositories
            kernel.Bind<IModelRepository<Address>>().To<ModelRepository<Address>>().InRequestScope();
            kernel.Bind<IModelRepository<Contact>>().To<ModelRepository<Contact>>().InRequestScope();

            //Services...
			kernel.Bind<IAutumnService<Contact>>().To<ContactsService>().InRequestScope();

        }
    }
}