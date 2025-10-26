using IA_RoboBerto.Autenticação;
using IA_RoboBerto.Contratos.ContratosRepositorio;
using IA_RoboBerto.Contratos.ContratosServicos;
using IA_RoboBerto.Middlewares;
using IA_RoboBerto.Modelos;
using IA_RoboBerto.Repositorio;
using IA_RoboBerto.Repositorio.Context;
using IA_RoboBerto.Servico;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

var key = Encoding.ASCII.GetBytes(Configuracoes.Secret);

// Add services to the container.
builder.Services.AddAuthentication(x =>
{
    var key = Encoding.ASCII.GetBytes(Configuracoes.Secret);

    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

        
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<RoboBertoContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddHttpClient<IGeminiServico, GeminiServico>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(10);
});

builder.Services.AddScoped<ITokenServico, TokenServico>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ISLAService, SLAService>();
builder.Services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();

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

builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "Roboberto", 
        Version = "v1",
        Description = @"
Essa API permite gerenciar usuários, departamentos, categorias e chamados, com controle de acesso baseado em roles (ADM, TECNICO e COLABORADOR) e autenticação via JWT.

💡 **Instruções de autenticação:**
- Para efetuar login, use o endpoint de login enviando o email do usuário e a senha padrão `senha123` (as senhas no banco estão armazenadas como hash).
- O token JWT retornado deve ser incluído no header `Authorization: Bearer {token}` para acessar endpoints protegidos.
- Use [jwt.io](https://jwt.io) para decodificar o token e verificar as claims:
    - `unique_name`: nome do usuário
    - `role`: função do usuário (ADM, TECNICO ou COLABORADOR)
    - `nbf`, `exp`, `iat`: datas de validade do token

🔒 **Controle de acesso por roles:**
- **ADM**: pode gerenciar departamentos, usuários, categorias e todos os chamados.
- **TECNICO**: pode visualizar e acatar chamados atribuídos a ele, interagir via chat com o colaborador.
- **COLABORADOR**: pode abrir, visualizar e tentar resolver seus próprios chamados.

⚠️ **Códigos de resposta relevantes:**
- `401 Unauthorized`: o token não foi fornecido ou está inválido.
- `403 Forbidden`: o usuário está autenticado, mas não tem permissão para acessar este recurso.

📝 **Dica para testes:**
- Os emails estão cadastrados no banco com a senha `senha123`.  
- Após login, sempre inclua o token JWT nos requests de endpoints que requerem autenticação ou roles específicas.
"

    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer'[space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<RoboBertoContext>();

    // tenta conectar e aplicar migrations pendentes
    if (await ctx.Database.CanConnectAsync())
    {
        var pending = (await ctx.Database.GetPendingMigrationsAsync()).ToList();
        if (pending.Any())
        {
            await ctx.Database.MigrateAsync();
        }

        await RoboBertoDbInitializer.SeedAsync(ctx);
    }
}


app.UseMiddleware(typeof(GlobalErrorHandlingMiddleware));

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
