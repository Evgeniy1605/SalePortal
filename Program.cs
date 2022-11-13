using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SalePortal.Data;
using SalePortal.DbConnection;
var supportedCultres = new[] { "en", "uk",  };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultres[0])
    .AddSupportedCultures(supportedCultres)
    .AddSupportedUICultures(supportedCultres);
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddServices();
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseRequestLocalization(localizationOptions);
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
