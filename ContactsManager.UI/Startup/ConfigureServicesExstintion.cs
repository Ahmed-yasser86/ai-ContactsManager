using Entities;
using Microsoft.EntityFrameworkCore;

using RepositryContracts;
using Serilog;
using ServiceContracts;

using Servicess;
using StocksApp2.ContactComponent.Filters;
using Repositories;
namespace StocksApp2.Startup
{
    public static class ConfigureServicesExstintion 
    {

        public static IServiceCollection ConfigureServices(this IServiceCollection Services, IConfiguration Configuration)
        {

         

           Services.AddControllersWithViews();

            // ========== REPOSITORY LAYER ==========
           Services.AddScoped<CountryRepositryContract, CountryRepository>();
           Services.AddScoped<PersonRepositryContract, PersonRepository>();

            // ========== SERVICE LAYER ==========
            // Register country services separately
          Services.AddScoped<ICountryGetterService, CountryGetterService>();
          Services.AddScoped<ICountryAdderService, CountryAdderService>();
            // Register each service separately
            Services.AddScoped<IPersonGetterService, PersonGetterService>();
           Services.AddScoped<IPersonAdderService, PersonAdderService>();
           Services.AddScoped<IPersonUpdaterService, PersonUpdaterService>();
           Services.AddScoped<IPersonDeleterService, PersonDeleterService>();
           Services.AddScoped<IPersonSearcherService, PersonSearcherService>();
           Services.AddScoped<IPersonSorterService, PersonSorterService>();

            // If you still need the old combined interface for backward compatibility
            // you can create a facade or keep the old PersonServices class
            // but it's better to let clients depend on the specific interfaces they need

            // Register Stocks Service (USE SCOPED, NOT SINGLETON!)


           Services.AddScoped<PerformanceLoggingFilter>();
            // For Stocks DbContext


            // ========== DATABASE CONTEXTS ==========
           Services.AddDbContext<AppDBContext>(
                Options => Options.UseSqlServer(Configuration.GetConnectionString("ContactDb"))
            );



            // ========== CONFIGURATION ==========
         

            // ========== HTTP LOGGING ==========
           Services.AddHttpLogging(options =>
            {
                options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestPropertiesAndHeaders | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
            });

            return Services;
        }

    }
}
