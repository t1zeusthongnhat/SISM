

using StudentManager.Services.Imp;
using StudentManager.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

// Đăng ký UserService
builder.Services.AddTransient<IUserService, UserService>();

// Đăng ký StudentService
builder.Services.AddScoped<IStudentService, StudentService>();

// Đăng ký CourseService
builder.Services.AddScoped<ICourseService, CourseService>();


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

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    
    pattern: "{controller=Course}/{action=onlyViewCourse}/{id?}");

app.Run();
