using Application.Services.Infrastructure;
using Autofac;
using Infrastructure.Cache;
using Infrastructure.Decorators;
using Infrastructure.Persistence.Commands.Employees;
using System.Reflection;

namespace Infrastructure
{
    public class AutofacInfrastructureModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var thisAssembly = Assembly.GetExecutingAssembly();

            builder
                .RegisterAssemblyTypes(thisAssembly)
                .AsClosedTypesOf(typeof(ICachingProvider<,>))
                .SingleInstance();

            //register retrievals
            builder
                .RegisterAssemblyTypes(thisAssembly)
                .AsClosedTypesOf(typeof(IEntityRetrieval<,>))
                .SingleInstance();
            builder
                .RegisterGenericDecorator(typeof(EntityRetrievalCachingPersistenceDecorator<,>), typeof(IEntityRetrieval<,>));

            builder
               .RegisterGeneric(typeof(CacheAdapter<,>))
               .As(typeof(ICacheAdapter<,>))
               .SingleInstance();


            builder
                .RegisterType<EmployeePersistence>()
                .As<IEmployeePersistence>()
                .SingleInstance();
            builder
                .RegisterDecorator(typeof(EmployeeCachingPersistenceDecorator), typeof(IEmployeePersistence));

            //register query persistence
            builder
                .RegisterAssemblyTypes(thisAssembly)
                .AsClosedTypesOf(typeof(IQueryPersistence<,>))
                .SingleInstance();
        }
    }
}
