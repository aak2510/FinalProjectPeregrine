using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RMSProject.Areas.Identity.Data;
using RMSProject.Data;
using Microsoft.Extensions.DependencyInjection;
using RMSProject.Repositories.IRepository;
using NuGet.Protocol.Core.Types;
using RMSProject.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("RMSProjectDbContextConnection") ?? throw new InvalidOperationException("Connection string 'RMSProjectDbContextConnection' not found.");

builder.Services.AddDbContext<RMSProjectDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<SystemUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<RMSProjectDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ModelsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ModelsDbContext") ?? throw new InvalidOperationException("Connection string 'ModelsDbContext' not found.")));


// Add the repository to the dependency container as we are using a service and we want to create it once instead of calling it in each class/controller implementation.
// Service Life time option is scoped, which means a new object is created each time one of the controller actions are executed and destroyed when a new action is executed
builder.Services.AddScoped<IMenuItemsRepository, MenuItemsRepository>();
builder.Services.AddScoped<INutritionalInformationRepository, NutritionalInformationRepository>();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
