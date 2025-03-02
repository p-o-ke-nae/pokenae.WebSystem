using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using pokenae.WebSystem.Infrastructure.Data;
using pokenae.WebSystem.Infrastructure.Repositories;
using pokenae.WebSystem.Infrastructure.Repositories.impl;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.WebUtilities;
using pokenae.WebSystem.Application.Interfaces;
using pokenae.WebSystem.API.Services.impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS�̐ݒ��ǉ�
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.None; // SameSite �̐ݒ�
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // HTTPS �݂̂� Cookie �𑗐M
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // �Z�b�V�����̃^�C���A�E�g�ݒ�
})
.AddGoogle(options =>
{
    var clientId = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID") ?? string.Empty;
    var clientSecret = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET") ?? string.Empty;
    options.ClientId = clientId;
    options.ClientSecret = clientSecret;
    options.CallbackPath = new PathString("/api/account/google-callback");
    options.Scope.Add("email"); // email �X�R�[�v��ǉ�
    options.Scope.Add("profile"); // profile �X�R�[�v��ǉ�
    options.SaveTokens = true; // �g�[�N����ۑ�

    // state �p�����[�^�̐���
    options.Events.OnRedirectToAuthorizationEndpoint = context =>
    {
        var state = Guid.NewGuid().ToString();
        context.Properties.Items["state"] = state;
        context.HttpContext.Response.Cookies.Append("OAuthState", state, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None
        });
        // ���_�C���N�g URL �� state �p�����[�^���܂܂�Ă��邩�m�F
        var redirectUri = context.RedirectUri;
        var uri = new Uri(redirectUri);
        var query = QueryHelpers.ParseQuery(uri.Query);

        if (!query.ContainsKey("state"))
        {
            // state �p�����[�^��ǉ�
            redirectUri = QueryHelpers.AddQueryString(redirectUri, "state", state);
        }

        context.Response.Redirect(redirectUri);

        return Task.CompletedTask;
    };
});


// Add DbContext
builder.Services.AddDbContext<WebSystemDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("pokenae_WebAppContext")));

// Add IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Add Repositories
builder.Services.AddScoped<IPageRepository, PageRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();

// Add Services
builder.Services.AddScoped<IPageService, PageService>();
builder.Services.AddScoped<AuthorizationService>();
builder.Services.AddScoped<IAccountService, AccountService>();

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

// CORS�̃~�h���E�F�A��ǉ�
app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();
