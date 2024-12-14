using AquaPestControlSystem.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Register services before calling builder.Build()
builder.Services.AddDbContext<ProprieterCustomerDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add authorization services
builder.Services.AddAuthorization();

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization(); // Requires AddAuthorization() in the services
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=UserLogin}/{id?}");

app.Run();
