using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TestSetup.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();


//builder.Services.Configure<IdentityOptions>(o =>
//{
//    o.Password.RequireDigit = true;
//    o.Password.RequireLowercase = true;
//    o.Password.RequireNonAlphanumeric = true;
//    o.Password.RequireUppercase = true;
//    o.Password.RequiredLength = 6;
//    o.Password.RequiredUniqueChars = 1;
//    o.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
//    o.Lockout.MaxFailedAccessAttempts = 5;
//    o.Lockout.AllowedForNewUsers = true;
//    o.User.RequireUniqueEmail = true;
//});



builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//builder.Services.AddIdentity<IdentityUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();



builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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

