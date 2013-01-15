using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace Autumn.Mvc.Infrastructure.IoC
{
    /// <summary>
    /// Provides the global scope implementation of the dependency resolver using Ninject.
    /// </summary>
    public class NinjectHttpDependencyResolver : NinjectDependencyScope, IDependencyResolver
    {
        private IKernel kernel;

        public NinjectHttpDependencyResolver(IKernel kernel)
            : base(kernel)
        {
            this.kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(kernel.BeginBlock());
        }
    }
}
