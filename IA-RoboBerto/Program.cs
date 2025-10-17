using IA_RoboBerto.Contratos.ContratosRepositorio;
using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.Repositorio;
using IA_RoboBerto.Repositorio.Context;
using IA_RoboBerto.Servico;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<RoboBertoContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<IRoleRepositorio,RoleRepositorio>(); 
builder.Services.AddScoped<IRoleServico,RoleServico>();

builder.Services.AddScoped<IDepartamentoServico, DepartamentoServico>();
builder.Services.AddScoped<IDepartamentoRepositorio, DepartamentoRepositorio>();

builder.Services.AddScoped<IUsuarioServico, UsuarioServico>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

builder.Services.AddScoped<ICategoriaServico, CategoriaServico>();
builder.Services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();

builder.Services.AddScoped<IChamadoServico, ChamadoServico>();
builder.Services.AddScoped<IChamadoRepositorio, ChamadoRepositorio>();

builder.Services.AddScoped<IMensagensServico, MensagensServico>();
builder.Services.AddScoped<IMensagensRepositorio, MensagensRepositorio>();


var app = builder.Build();

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
