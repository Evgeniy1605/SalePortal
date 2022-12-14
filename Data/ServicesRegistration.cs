using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using SalePortal.Domain;
using System.Globalization;

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
                option.AccessDeniedPath = "/denied";
            });
        services.AddAutoMapper(typeof(Program).Assembly);
        services.AddTransient<ICategoryHttpClient,CategoryHttpClient>();
        services.AddTransient<ILibrary, Library>();
        services.AddTransient<IIdentityLibrary, IdentityLibrary>();
        services.AddTransient<IUserHttpClient, UserHttpClient>();
        services.AddTransient<ICommodityHttpClient, CommodityHttpClient>();
        services.AddTransient<IOrderCommodity, OrderCommodity>();
        services.AddTransient<IChat, Chat>();
        services.AddLocalization(opt => { opt.ResourcesPath = "Resouces"; });
        services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();
        services.Configure<RequestLocalizationOptions>(opt => 
        {
            var suportedCultures = new List<CultureInfo>()
            {
                new CultureInfo("en"),
                new CultureInfo("uk")
            };
            opt.DefaultRequestCulture = new RequestCulture("en");
            opt.SupportedCultures= suportedCultures;
            opt.SupportedUICultures= suportedCultures;
        });
        services.AddSignalR();
        services.AddTransient<IOrderHttpClient, OrderHttpClient>();
        services.AddTransient<IMessageHttpClient, ChatHttpClient>();
        services.AddTransient<IChatHttpClient, ChatHttpClient>();


        return services;
    }
}
