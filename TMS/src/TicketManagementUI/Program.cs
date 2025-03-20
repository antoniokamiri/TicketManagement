using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using TicketManagementUI.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);

// Register Identity services
builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<AppDBContext>()
    .AddSignInManager();

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.LoginPath = "/login";
    opt.AccessDeniedPath = "/accessdenied";
});

// Register DbContext with Identity
builder.Services.AddDbContext<AppDBContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Register Services
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
