
using CapaNegocio.Contracts;
using CapaNegocio.Bissness;
using Microsoft.EntityFrameworkCore;
using CapaDatos.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPersona, PersonaBissness>();
builder.Services.AddDbContext<DasignoContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL")));

var CorsReglas = "ReglasCors";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: CorsReglas, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors(CorsReglas);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
