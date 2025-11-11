using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FootTrack.Models;
using FootTrack.Data;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
    "Server=localhost;Database=FootTrackDb;User Id=sa;Password=MyStr0ng!Pass;TrustServerCertificate=True;";


builder.Services.AddDbContext<FootTrackContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<Uporabnik>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true; 
}).AddEntityFrameworkStores<FootTrackContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<FootTrackContext>();
    var userManager = services.GetRequiredService<UserManager<Uporabnik>>();
    await DbInitializer.Initialize(context, userManager);
}

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
