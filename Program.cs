using _102190334_NguyenMinhQuang.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("DataSource=HD.db"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var sv = scope.ServiceProvider;
    try
    {
        var context = sv.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();
    }
    catch (Exception e)
    {
        var logger = sv.GetRequiredService<ILogger<Program>>();
        logger.LogError(e, "An error occurred while migrating or seeding the database.");
    }
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

app.Run();
