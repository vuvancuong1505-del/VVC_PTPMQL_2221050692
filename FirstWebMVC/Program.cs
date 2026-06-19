using Microsoft.EntityFrameworkCore;
using FirstWebMVC.Data;
using FirstWebMVC.Models;
using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;

var builder = WebApplication.CreateBuilder(args);

// Set EPPlus License for non-commercial use
try
{
    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
}
catch
{
    // License context may already be set or not supported in this version
}
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("ApplicationDbContext") ?? throw new 
    InvalidOperationException("Connection string 'ApplicationDbContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();

    if (!db.Faculties.Any())
    {
        db.Faculties.AddRange(
            new FirstWebMVC.Models.Faculty { Name = "Công nghệ thông tin" },
            new FirstWebMVC.Models.Faculty { Name = "Quản trị kinh doanh" },
            new FirstWebMVC.Models.Faculty { Name = "Kinh tế" }
        );
        db.SaveChanges();
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
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Demo}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
