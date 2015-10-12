using System.Reflection;
using Autofac;
using SYDQ.Infrastructure.Configuration;
using SYDQ.Infrastructure.Domain;
using SYDQ.Infrastructure.Email;
using SYDQ.Infrastructure.Logging;
using SYDQ.Infrastructure.UnitOfWork;
using SYDQ.Repository.EF;

namespace SYDQ.ConsoleApp
{
    public class AutofacBooter
    {
        private static IContainer _container;
        public static T GetInstance<T>()
        {
            return _container.Resolve<T>();
        }

        public static void Run()
        {
            SetAutofacContainer();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            SetupResolveRules(builder);

            _container = builder.Build();

            EntitiesContextFactory.Init(_container.Resolve<IEntitiesContextStorageContainer>());
            ApplicationSettingsFactory.InitializeApplicationSettingsFactory(_container.Resolve<IApplicationSettings>());

        }

        private static void SetupResolveRules(ContainerBuilder builder)
        {
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
