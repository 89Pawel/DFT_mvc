using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DFT_MVC.Data;
using Microsoft.AspNetCore.Hosting;
using DFT_MVC.Services;
using DFT_MVC.ViewModel;

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.ConfigureKestrel(serverOptions =>
//{
//    serverOptions.Limits.MaxRequestBodySize = long.MaxValue;
//});


builder.Services.AddDbContext<DFT_MVC_Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DFT_MVC_Context") ?? throw new InvalidOperationException("Connection string 'DFT_MVC_Context' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IImageService, ImageService>();
builder.Services.AddTransient<IKategorieService, KategorieViewModel>();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Services.CreateScope().ServiceProvider.GetRequiredService<DFT_MVC_Context>().Database.Migrate();

app.Run();
