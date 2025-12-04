using System.Linq;
using System.IO;
using Serilog;
using Serilog.Events;
using HospitalAutomation.Web.Filters;
using HospitalAutomation.Data;
using HospitalAutomation.Data.Interfaces;
using HospitalAutomation.Models;
using HospitalAutomation.Models.Enums;
using HospitalAutomation.Services;
using HospitalAutomation.Services.Interfaces;
using HospitalAutomation.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using HospitalAutomation.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog for file + console logging
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(Path.Combine(AppContext.BaseDirectory, "logs", "hospital-.log"), rollingInterval: RollingInterval.Day, retainedFileCountLimit: 31, shared: true)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container and register logging filter
builder.Services.AddScoped<LoggingActionFilter>();
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<LoggingActionFilter>();
});

// Database Configuration
var connectionString = builder.Configuration.GetConnectionString("HospitalConnectionString");
builder.Services.AddDbContext<HospitalDbContext>(options =>
    options.UseSqlite(connectionString));

// HttpContextAccessor for SessionManager
builder.Services.AddHttpContextAccessor();

// UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

// HttpContextAccessor is already registered above, no need to register again

// Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });

// Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("StaffOnly", policy => policy.RequireRole("Admin", "Doctor", "Nurse", "Receptionist"));
    options.AddPolicy("DoctorOnly", policy => policy.RequireRole("Doctor", "Admin"));
    options.AddPolicy("NurseOnly", policy => policy.RequireRole("Nurse", "Admin"));
});

// Initialize LogHelper for legacy components that rely on file logging
LogHelper.Initialize();

var app = builder.Build();

// Global Exception Handling Middleware
app.UseMiddleware<ExceptionMiddleware>();

// Ensure database is created and seed initial data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<HospitalDbContext>();
        
        // Initialize database
        DbInitializer.Initialize(context);
        
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("Database initialized and seeded successfully.");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

// Initialize SessionManager and AuthorizationHelper
app.Use(async (context, next) =>
{
    var httpContextAccessor = context.RequestServices.GetRequiredService<IHttpContextAccessor>();
    SessionManager.SetHttpContextAccessor(httpContextAccessor);
    AuthorizationHelper.SetHttpContextAccessor(httpContextAccessor);
    await next();
});

// Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Home/Error");
//     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//     app.UseHsts();
// }
app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
