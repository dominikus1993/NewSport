using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NewSport.Domain.Api;
using NewSport.Domain.Concrete;
using Ninject;

namespace NewSport.WebApi.Infrastructure
{
    class DefaultDependencyResolver:IDependencyResolver
    {
        private IKernel _kernel;

        public DefaultDependencyResolver()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }

        private void AddBindings()
        {
            _kernel.Bind<IEncryptProvider>().To<DefaultEncryptProvider>();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
    }
}
