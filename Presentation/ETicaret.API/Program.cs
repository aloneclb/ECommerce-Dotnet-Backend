using System.Data;
using System.Text;
using ETicaret.API.Extensions;
using ETicaret.Application;
using Eticaret.Infrastructure;
using Eticaret.Infrastructure.Services.Storage.Local;
using ETicaret.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddDefaultPolicy(
    policy => policy
        .WithOrigins("http://localhost:3000", "https://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod()
));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // Beraer eki buradan geliyor.
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience =
                true, // Tokenin Kimlerin yada hangi sitelerin kullanacağını belirttiğimiz değer. "ORN www.myclient.com yani kendi clientim"
            ValidateIssuer = true, // Tokenin kimin verdiğini söyleyen değer. "ORN: www.myapi.com yani kendi sitem" 
            ValidateLifetime = true, // Tokenin geçerlilik süresinin saklandığı alandır bunuda kontrole alıyoruz.
            ValidateIssuerSigningKey =
                true, // Üretilecek token değerinin uygulamamıza ait bir değer olduğunu ifade eden securiry key verisidir. Yani özel anahtar.

            ValidAudience = builder.Configuration["JWTToken:Audience"],
            ValidIssuer = builder.Configuration["JWTToken:Issuer"],
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTToken:SecurityKey"]!)),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
                expires != null && expires > DateTime.UtcNow,
            // süresini kontrol etme delegate'i
            
        };
    });


// Serilog
Logger log = new LoggerConfiguration()
    // .WriteTo.Console()
    // .WriteTo.File("logs/log.txt")
    // // .WriteTo.Seq("server url") Seq 
    .WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("SQLServer"),
        tableName: "logs",
        autoCreateSqlTable: true
        // columnOptions: new ColumnOptions
        // {
        //     AdditionalColumns = new List<SqlColumn>()
        //     {
        //         new SqlColumn()
        //             { ColumnName = "email", DataType = SqlDbType.VarChar, AllowNull = true, PropertyName = "email" }
        //     } burası işin içine girince logg atılmıyor veritabanına bazen ?
        // }
        )
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();
builder.Host.UseSerilog(log);


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

// Global exception middleware
// app.UseGlobalExceptionHandler(app.Services.GetRequiredService<ILogger<Program>>());
app.UseGlobalExceptionHandler();

// For wwwroot
app.UseStaticFiles();

// Serilog
app.UseSerilogRequestLogging();

// Cors MiddleWare
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Serilog'da log context bilgilerini besleme
app.Use(async (context, next) =>
{
    var email = context.User?.Identity?.IsAuthenticated != null || true
        ? context.User.Claims
            .FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value
        : null;

    LogContext.PushProperty("email", email);
    await next();
});

app.MapControllers();


app.Run();