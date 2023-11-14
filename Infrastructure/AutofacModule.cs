using Application.Services.Infrastructure;
using Autofac;
using Infrastructure.Persistence.Commands.Employees;
using System.Reflection;

namespace Infrastructure
{
    public class AutofacInfrastructureModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var thisAssembly = Assembly.GetExecutingAssembly();

            //register retrievals
            builder
                .RegisterAssemblyTypes(thisAssembly)
                .AsClosedTypesOf(typeof(IEntityRetrieval<,>))
                .SingleInstance();

            builder
                .RegisterType<EmployeePersistence>()
                .As<IEmployeePersistence>()
                .SingleInstance();

            //register query persistence
            builder
                .RegisterAssemblyTypes(thisAssembly)
                .AsClosedTypesOf(typeof(IQueryPersistence<,>))
                .SingleInstance();
        }
    }
}
