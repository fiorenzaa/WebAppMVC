using Microsoft.EntityFrameworkCore;
using WebAppMVC.Data;
using WebAppMVC.Services;

var builder = WebApplication.CreateBuilder(args);
// Konfigurasi koneksi database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??

throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

//----------------------------- Block Service ----------------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)); // Gunakan UseSqlServer untuk SQL Server

//Register DI pasti di block service
// Add services to the container.
builder.Services.AddControllersWithViews();

// ✅ REGISTER IN-MEMORY DATABASE
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseInMemoryDatabase("WebAppMVCDb"));

// Daftarkan StudentService sebagai Scoped service
// builder.Services.AddScoped<IStudentService, StudentService>();
// builder.Services.AddScoped<IAttendanceService, AttendanceService>();


//----------------------------- Block App --------------------------------
var app = builder.Build();

// ✅ SEED DATA (isi database dengan data awal)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate(); // Menerapkan migrasi yang tertunda
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
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
    pattern: "{controller=Student}/{action=Index}/{id?}");

app.Run();
