using Web_Application_Expense_Tracker.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//Dependency Injections
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo + DSMBMAY9C3t2UVhhQlVFfV5AQmBIYVp / TGpJfl96cVxMZVVBJAtUQF1hTX5SdkdjXntfdHVdRGVY");

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
