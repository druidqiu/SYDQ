using Autofac;
using SYDQ.Infrastructure.Configuration;
using SYDQ.Infrastructure.Domain;
using SYDQ.Infrastructure.Email;
using SYDQ.Infrastructure.Logging;
using SYDQ.Infrastructure.UnitOfWork;
using SYDQ.Repository.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using SYDQ.Infrastructure.Web.Authentication;

namespace SYDQ.Web.App_Start
{
    public class AutofacConfig
    {
        //private static IContainer _container;
        public static T GetInstance<T>()
        {
            return DependencyResolver.Current.GetService<T>();
            //return _container.Resolve<T>();
        }

        public static void RegisterAll()
        {
            SetAutofacContainer();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();

            SetupResolveRules(builder);

            IContainer _container = builder.Build();

            //GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);//for web api
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));

            EntitiesContextFactory.Init(_container.Resolve<IEntitiesContextStorageContainer>());
            ApplicationSettingsFactory.InitializeApplicationSettingsFactory(_container.Resolve<IApplicationSettings>());
            LoggingFactory.InitializeLogFactory(_container.Resolve<ILogger>());
        }

        private static void SetupResolveRules(ContainerBuilder builder)
        {
            builder.RegisterControllers(Assembly.Load("SYDQ.Controllers"));

            builder.RegisterGeneric(typeof(EntityRepository<>)).As(typeof(IRepository<>)).InstancePerDependency();
            
            builder.RegisterAssemblyTypes(Assembly.Load("SYDQ.Repository.EF"))
                .Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces();
            
            builder.RegisterAssemblyTypes(Assembly.Load("SYDQ.Services"))
                .Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();

            builder.RegisterType<ThreadStorageContainer>().As<IEntitiesContextStorageContainer>().SingleInstance();
            builder.RegisterType<AppConfigApplicationSettings>().As<IApplicationSettings>().SingleInstance();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<Log4NetAdapter>().As<ILogger>().SingleInstance();
            builder.RegisterType<TextLoggingEmailService>().As<IEmailService>().SingleInstance();
            builder.RegisterType<AspFormsAuthentication>().As<IFormsAuthentication>();

            #region template
            //builder.RegisterType<HttpContextCacheAdapter>().As<ICacheStorage>();
            //builder.RegisterType<CachedRoleService>().Named<IRoleService>("cachedRoleService");
            //builder.Register(c => new UserController(c.ResolveNamed<IRoleService>("cachedRoleService"), c.Resolve<IControllerInitialization>()));
            //builder.RegisterType<CachedPeriodService>().Named<IPeriodService>("cachedPeriodService");
            //builder.Register<IControllerInitialization>(c => new ControllerInitialization(c.Resolve<ICookieStorageService>(), c.Resolve<IUserService>(), c.ResolveNamed<IPeriodService>("cachedPeriodService")));
            #endregion template
        }
    }
}