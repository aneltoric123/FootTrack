using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FootTrack.Models;
using FootTrack.Data;

DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);

var adminEmail = Environment.GetEnvironmentVariable("ADMIN_EMAIL");
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("Connection string not found! Check Azure App Settings.");
}

builder.Services.AddDbContext<FootTrackContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.ExpireTimeSpan = TimeSpan.FromHours(1);
    options.SlidingExpiration = true;
});
builder.Services.AddDefaultIdentity<Uporabnik>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true; 
}).AddEntityFrameworkStores<FootTrackContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<LeaderboardService>();

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

// Run DbInitializer via DI
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<FootTrackContext>();
    var userManager = services.GetRequiredService<UserManager<Uporabnik>>();
    await DbInitializer.Initialize(context, userManager); // must be awaited
}

// Middleware
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

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
