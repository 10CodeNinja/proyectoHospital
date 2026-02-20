using Hosp.Models;
using Hosp.Repositorio;
using Hosp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();



builder.Services.AddScoped<MedicoServicio>();
builder.Services.AddScoped<PacienteServicio>();
builder.Services.AddScoped<MedicoRepositorio>();
builder.Services.AddScoped<PacienteRepositorio>();

builder.Services.AddDbContext<HospitalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));

var app = builder.Build();




app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();