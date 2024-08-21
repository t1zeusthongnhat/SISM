using StudentManager.Services.Imp;
using StudentManager.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using StudentManager.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromSeconds(40);
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

// Authentication configuration
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/Login";
    });

// Register UserService
builder.Services.AddTransient<IUserService, UserService>();

// Register StudentService
builder.Services.AddScoped<IStudentService, StudentService>();

// Register CourseService
builder.Services.AddScoped<ICourseService, CourseService>();

// Register RoleAuthorize
builder.Services.AddScoped<RoleAuthorize>();

var app = builder.Build();

// Configure the HTTP request pipeline.
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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();