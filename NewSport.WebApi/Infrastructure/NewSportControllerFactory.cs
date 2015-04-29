using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using NewSport.Domain.Api;
using NewSport.Domain.Concrete;
using NewSport.Domain.Entities;
using NewSport.Domain.Entity;
using Ninject;

namespace NewSport.WebApi.Infrastructure
{
    public class NewSportControllerFactory : DefaultControllerFactory
    {
        private IKernel _defaultKernel;

        public NewSportControllerFactory()
        {
            _defaultKernel = new StandardKernel();
            AdBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return (IController) (controllerType == null ? null : _defaultKernel.Get(controllerType));
        }

        private void AdBindings()
        {
            _defaultKernel.Bind<IUserRepository>().To<DefaultUserRepository>();
            _defaultKernel.Bind<IPostRepository>().To<DefaultPostRepository>();
        }
    }
}