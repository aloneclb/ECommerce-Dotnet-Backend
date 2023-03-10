using ETicaret.Application;
using Eticaret.Infrastructure;
using Eticaret.Infrastructure.Services.Storage.Local;
using ETicaret.Persistence;

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

app.UseAuthorization();

app.MapControllers();

app.Run();