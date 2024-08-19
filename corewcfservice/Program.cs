// Startup.cs
using CoreWCF;
using CoreWCF.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;









public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddServiceModelServices();
        services.AddServiceModelMetadata();
        services.AddSingleton<IAccountService, AccountService>();
        services.AddSingleton<IAddressService, AddressService>();
        services.AddSingleton<IAccountHolderService, AccountHolderService>();
        services.AddSingleton<ITransactionService, TransactionService>();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseServiceModel(builder =>
        {
            builder.AddService<AccountService>();
            builder.AddService<AddressService>();
            builder.AddService<AccountHolderService>();
            builder.AddService<TransactionService>();

            builder.AddServiceEndpoint<AccountService, IAccountService>(
                new BasicHttpBinding(),
                "/AccountService.svc"
            );

            builder.AddServiceEndpoint<AddressService, IAddressService>(
                new BasicHttpBinding(),
                "/AddressService.svc"
            );

            builder.AddServiceEndpoint<AccountHolderService, IAccountHolderService>(
                new BasicHttpBinding(),
                "/AccountHolderService.svc"
            );

            builder.AddServiceEndpoint<TransactionService, ITransactionService>(
                new BasicHttpBinding(),
                "/TransactionService.svc"
            );
        });
    }
}





namespace corewcfservice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices(services =>
                    {
                        services.AddServiceModelServices();

                        // Register LookupService with dependency injection
                        services.AddSingleton<ILookupService, LookupService>(provider =>
                        {
                            var connectionString = "Server=SURYA\\SQLEXPRESS;Database=VueJsApp;Integrated Security=True";
                            return new LookupService(connectionString);
                        });

                        // Add CORS policy
                        services.AddCors(options =>
                        {
                            options.AddPolicy("AllowAll", builder =>
                            {
                                builder.AllowAnyOrigin()
                                       .AllowAnyMethod()
                                       .AllowAnyHeader();
                            });
                        });

                        // Add service metadata to enable WSDL and MEX
                        services.AddServiceModelMetadata();
                    })
                    .Configure(app =>
                    {
                        // Enable CORS
                        app.UseCors("AllowAll");

                        // Set up routing
                        app.UseRouting();

                        // Configure service model with endpoints and metadata
                        app.UseServiceModel(builder =>
                        {
                            builder.AddService<LookupService>();

                            // Add endpoint for the LookupService
                            builder.AddServiceEndpoint<LookupService, ILookupService>(
                                new BasicHttpBinding(), "/LookupService.svc");

                            // Enable WSDL and MEX (Metadata Exchange)
                            var serviceMetadataBehavior = app.ApplicationServices.GetRequiredService<ServiceMetadataBehavior>();
                            serviceMetadataBehavior.HttpGetEnabled = true;
                            serviceMetadataBehavior.HttpGetUrl = new Uri("/LookupService.svc?wsdl", UriKind.Relative);
                        });

                        // Use endpoints for routing WCF services
                        app.UseEndpoints(endpoints =>
                        {
                            // This handles any other endpoints you may need for routing (non-WCF related)
                        });
                    });
                });
    }
}
