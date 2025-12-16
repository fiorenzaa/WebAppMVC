using WebAppMVC.Services; 

var builder = WebApplication.CreateBuilder(args);

//----------------------------- Block Service ----------------------------
//Register DI pasti di block service
// Add services to the container.
builder.Services.AddControllersWithViews();
// Daftarkan StudentService sebagai Scoped service
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();


//----------------------------- Block App --------------------------------
var app = builder.Build();

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
