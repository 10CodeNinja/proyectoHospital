using Hosp.Models;
using Hosp.Repositorio;
using Hosp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Hosp API",
        Version = "v1"
    });
});


builder.Services.AddScoped<MedicoServicio>();
builder.Services.AddScoped<PacienteServicio>();
builder.Services.AddScoped<MedicoRepositorio>();
builder.Services.AddScoped<PacienteRepositorio>();


builder.Services.AddDbContext<HospitalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));

var app = builder.Build();


app.UseHttpsRedirection();
app.UseAuthorization();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hosp API V1");
        c.RoutePrefix = "swagger";
    });
}

app.MapControllers();
app.Run();