using System;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using VisitorLog.ApplicationDBContextService;
using VisitorLog.Services.Auth;
using VisitorLog.Services.QRCodeManagementService;
using VisitorLog.Services.QRSetManagementService;
using VisitorLog.Services.VisitorManagementService;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("default")));

builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor(options =>
{
    options.DetailedErrors = builder.Environment.IsDevelopment();
    options.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(3);
    options.JSInteropDefaultCallTimeout = TimeSpan.FromMinutes(1);
}).AddHubOptions(o => o.MaximumReceiveMessageSize = 100_000_000);

// auth state provider registration
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<SimpleAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<SimpleAuthStateProvider>());

builder.Services.AddScoped<IQRCodeManagementService, QRCodeManagementService>();
builder.Services.AddScoped<IVisitorManagementService, VisitorManagementService>();
builder.Services.AddScoped<IQRSetManagementService, QRSetManagementService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
