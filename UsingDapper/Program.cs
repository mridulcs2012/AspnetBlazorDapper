using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.SqlDataAccess;
using DataAccessLibrary.Models;
using BlazorDapper.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddTransient<ISqlDataAcess, SqlDataAcess>();
builder.Services.AddTransient<IAdminData, AdminData>();
builder.Services.AddTransient<IAdminLogin, AdminLogin>();
builder.Services.AddTransient<IEmployeeServices, EmployeeServices>();
builder.Services.AddTransient<IAppointmentServices, AppointmentServices>();
builder.Services.AddTransient<IShareToServices, ShareToServices>();
builder.Services.AddTransient<IReportServices, ReportServices>();


builder.Services.AddSingleton<HttpClient>(); // Required for RDLC


builder.Services.AddScoped<SessionState>();
builder.Services.AddScoped<OtpSessionState>();

// Add services to the container for Cookie.
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers(); //Required For RDLC Report
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
