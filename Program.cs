using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StoreProject.Common.Services;
using StoreProject.Entities;
using StoreProject.Features.Admin.Services;
using StoreProject.Features.Category.Services;
using StoreProject.Features.User.Services;
using StoreProject.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        //options.ViewLocationFormats.Clear();
        //options.ViewLocationFormats.Add("/Features/{1}/Views/{0}.cshtml");
        //options.ViewLocationFormats.Add("/Features/Shared/{0}.cshtml");
        options.ViewLocationExpanders.Add(new FeatureViewLocationExpander());
    });

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IUserManagementService, UserManagementService>();
builder.Services.AddScoped<IFileManager, FileManager>();

builder.Services.AddDbContext<StoreContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"CONNECTION STRING: [{connStr}]");  // check output!

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<StoreContext>()
    .AddDefaultTokenProviders()
    .AddErrorDescriber<IdentityErrorDescriber>();

builder.Services.ConfigureApplicationCookie(option =>
{
    option.LoginPath = "/Features/User/Controller/AuthController/Login";
    option.LogoutPath = "/Features/User/Controller/AuthController/Logout";
});

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    await ApplicationDbInitializer.SeedRolesAndAdminAsync(services);
//}

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
