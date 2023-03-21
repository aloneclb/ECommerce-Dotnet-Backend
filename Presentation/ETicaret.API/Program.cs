using System.Security.Claims;
using System.Text;
using ETicaret.Application;
using Eticaret.Infrastructure;
using Eticaret.Infrastructure.Services.Storage.Local;
using ETicaret.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddDefaultPolicy(
    policy => policy
        .WithOrigins("http://localhost:3000","https://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod()
));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = true, // Tokenin Kimlerin yada hangi sitelerin kullanacağını belirttiğimiz değer. "ORN www.myclient.com yani kendi clientim"
            ValidateIssuer = true, // Tokenin kimin verdiğini söyleyen değer. "ORN: www.myapi.com yani kendi sitem" 
            ValidateLifetime = true, // Tokenin geçerlilik süresinin saklandığı alandır bunuda kontrole alıyoruz.
            ValidateIssuerSigningKey = true, // Üretilecek token değerinin uygulamamıza ait bir değer olduğunu ifade eden securiry key verisidir. Yani özel anahtar.
            
            ValidAudience = builder.Configuration["JWTToken:Audience"],
            ValidIssuer = builder.Configuration["JWTToken:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTToken:SecurityKey"]!)),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null && expires > DateTime.UtcNow,
            // süresini kontrol etme delegate'i
            
        };
    });


// Application Register
builder.Services.AddApplicationServices();
// Persistence Register
builder.Services.AddPersistanceServices();
// Insfrastructure Register
builder.Services.AddInfrastructureServices();
builder.Services.AddStorage<LocalStorage>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// For wwwroot
app.UseStaticFiles();

// Cors MiddleWare
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();