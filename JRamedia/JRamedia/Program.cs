using JRamedia.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt => {
    opt.LoginPath = "/Auth/Login";
    opt.ExpireTimeSpan = TimeSpan.FromMinutes(15);
});
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromMinutes(1);
});
builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.Name = "cookie";
    opt.ExpireTimeSpan = TimeSpan.FromSeconds(10);
    opt.SlidingExpiration = true;
});
builder.Services.AddControllers(
 options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);


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
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "/{controller=Auth}/{action=Login}/{id?}");

app.MapControllers();

app.Run();
