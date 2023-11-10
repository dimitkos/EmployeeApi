using Common;
using Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //when i add autofac
            //https://stackoverflow.com/questions/69754985/adding-autofac-to-net-core-6-0-using-the-new-single-file-template
            //builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            //        .ConfigureContainer<ContainerBuilder>(builder =>
            //        {
            //            builder.RegisterModule(new AutofacBusinessModule());
            //        });

            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            Configure(app);
        }

        private static void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddControllers();
            services.AddAuthorization();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var apiInstanceSettings = configuration.GetSection("ApiInstance").Get<ApiInstanceSettings>();
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
#warning check how validate this
            services.Configure<ApiInstanceSettings>(configuration.GetSection("ApiInstanceSettings"));
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