using Core.Interfaces;
using Core.Models;
using Core.Services;
using Datalayer;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'DefaultConnection' was not found.");

// Add services to the container.
builder.Services.AddScoped<SongService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<UserService>();

//Add repositories to the container
builder.Services.AddSingleton<ISongRepository>(new SongRepository(connectionString));
builder.Services.AddSingleton<IUserRepository>(new UserRepository(connectionString));

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication("SoundlyCookie")
    .AddCookie("SoundlyCookie", options =>
    {
        options.LoginPath = "/user/login";
        options.AccessDeniedPath = "/user/access-denied";
        
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();