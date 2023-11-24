using Application;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common;
using FluentValidation;
using Infrastructure;
using Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host
                .UseSerilog((context, configuration) =>
                        configuration.ReadFrom.Configuration(context.Configuration))
                .UseServiceProviderFactory<ContainerBuilder>(new AutofacServiceProviderFactory())
                    .ConfigureContainer((Action<ContainerBuilder>)(builder =>
                    {
                        RegisterAutofacModules(builder);
                    }));

            var serilogLogger = new LoggerConfiguration()
                    .CreateLogger();

            serilogLogger.Information("");

            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            Configure(app);
        }

        private static void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddControllers();
            services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(AutofacApplicationModule)));
            services.AddAuthorization();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var apiInstanceSettings = configuration.GetSection(nameof(ApiInstanceSettings)).Get<ApiInstanceSettings>();
#warning configure id gen

            RegisterConfiguration(services, configuration);
            RegisterDatabase(services, configuration);
            RegisterCaching(services);
        }


        private static void Configure(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }

        private static void RegisterConfiguration(IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddOptions<ApiInstanceSettings>().Bind(configuration.GetSection(nameof(ApiInstanceSettings))).ValidateDataAnnotations();
            services.AddOptions<CacheSettings>().Bind(configuration.GetSection(nameof(CacheSettings))).ValidateDataAnnotations();
        }

        private static void RegisterAutofacModules(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacApiModule());
            builder.RegisterModule(new AutofacApplicationModule());
            builder.RegisterModule(new AutofacInfrastructureModule());
        }

        private static void RegisterDatabase(IServiceCollection services, ConfigurationManager configuration)
        {
            var connectionString = configuration.GetConnectionString(Constants.Databases.Employees);

            services
                .AddDbContext<EmployeeDbContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Transient);
        }

        private static void RegisterCaching(IServiceCollection services)
        {
#warning
            //keep in mind to choose if want redis or memorycache
            services.AddMemoryCache();
        }
    }
}