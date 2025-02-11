using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using pokenae.WebSystem.Infrastructure.Data;
using pokenae.WebSystem.API.Services.impl;
using pokenae.WebSystem.Infrastructure.Repositories;
using pokenae.WebSystem.API.Services;
using pokenae.WebSystem.Infrastructure.Repositories.impl;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.Google;
using pokenae.WebSystem.Core.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add authentication services
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; // ここでデフォルトのサインインスキームを指定
})
.AddCookie()
.AddGoogle(options =>
{
    var clientId = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID");
    var clientSecret = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET");

    // Log the environment variables
    var logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();
    logger.LogInformation("GOOGLE_CLIENT_ID: {ClientId}", clientId);
    logger.LogInformation("GOOGLE_CLIENT_SECRET: {ClientSecret}", clientSecret);

    options.ClientId = clientId;
    options.ClientSecret = clientSecret;
    options.SaveTokens = true;
    options.CallbackPath = new PathString("/api/account/google-callback"); // ここでリダイレクト URI を設定
});

// Add DbContext
builder.Services.AddDbContext<WebSystemDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("pokenae_WebAppContext")));

// Add IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Add Repositories
builder.Services.AddScoped<IPageRepository, PageRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IPageCategoryRepository, PageCategoryRepository>();

// Add Services
builder.Services.AddScoped<IPageService, PageService>();
builder.Services.AddScoped<AuthorizationService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPageCategoryService, PageCategoryService>(); // ここで IPageCategoryService を登録

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
