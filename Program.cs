using InventorySandbox.Models;
using InventorySandbox.Models.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// Configure SQL Server Configuration
builder.Services.ConfigurePersistenceServices(builder.Configuration);

// Add authentication and authorization
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Identity/ApplicationUserLoginPages/Login";
        options.LogoutPath = "/Identity/ApplicationUserLoginPages/Logout"; // Set logout URL
        options.AccessDeniedPath = "/Identity/ApplicationUserLoginPages/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);  // Expiry time
        options.SlidingExpiration = true;  // Enable sliding expiration
    });


builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddScoped<IPasswordHasher<ApplicationUser>, PasswordHasher<ApplicationUser>>();

var app = builder.Build();

// Apply migrations
await PersistenceServiceRegistration.ApplyMigrations(app.Services);

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Map Razor Pages (Including Login page)
app.MapRazorPages();

// Ensure the login page is the fallback when no other route matches
// Only do this if you really want a fallback to the Login page
// Otherwise, remove this line if it's unnecessary
app.MapFallbackToPage("/Identity/ApplicationUserLoginPages/Login");

app.Run();