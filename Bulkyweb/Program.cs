using BulkyBookweb.Areas.Admin.Controllers;
using BulkyBook.DateAccess.Data;
using Microsoft.EntityFrameworkCore;
using BulkyBook.DateAccess.Repository;
using BulkyBook.DateAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using BulkyBook.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

IServiceCollection serviceCollection = builder.Services.AddDbContext<ApplactionContext>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString("Default")
    ));

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/login";
    options.LogoutPath = $"/Identity/Account/loginout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenited";
});
builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<ApplactionContext>().AddDefaultTokenProviders();

builder.Services.AddRazorPages();
builder.Services.AddScoped<ICategoryRepository,CategoryRepoitory>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

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
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
