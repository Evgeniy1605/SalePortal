using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using SalePortal.DbConnection;

namespace SalePortal.Data;

public static class ServicesRegistration
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddDbContext<SalePortalDbConnection>(options =>
        {
            options.UseSqlServer( WebApplication.CreateBuilder().Configuration.GetConnectionString("Connection"));
        });
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(option =>
            {
                option.LoginPath = "/login";
            });
        services.AddAutoMapper(typeof(Program).Assembly);
        services.AddTransient<ILibrary, Library>();

        services.AddLocalization(opt => { opt.ResourcesPath = "Resouces"; });
        services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();

        return services;
    }
}
