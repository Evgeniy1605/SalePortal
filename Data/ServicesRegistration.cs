using Microsoft.AspNetCore.Authentication.Cookies;
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
        return services;
    }
}
