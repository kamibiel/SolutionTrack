using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SolutionTrack.Dominio.Configuracoes;
using SolutionTrack.Dominio.DTOs;
using SolutionTrack.Dominio.Interfaces;
using SolutionTrack.Dominio.ModelViews;
using SolutionTrack.Dominio.Servicos;
using SolutionTrack.Infraestrutura.Db;
using SolutionTrack.Infraestrutura.Mappings;

#region Builder
var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
  .AddJwtBearer(options =>{
    var jwtConfig = builder.Configuration.GetSection("JwtConfig").Get<JwtConfig>();
    var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);

    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
      ValidateIssuer = false,
      ValidateAudience = false,
      ValidateLifetime = true,
      IssuerSigningKey = new SymmetricSecurityKey(key),
      ClockSkew = TimeSpan.Zero
    };
  });

builder.Services.AddSingleton<JwtService>();

builder.Services.AddControllers();

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Solution Track", Version = "v1" });

        // Configuração para adicionar o botão de autorização no Swagger
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Insira o token JWT neste formato: Bearer {seu token}"
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

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAuthorization();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
  options.UseMySql(
    builder.Configuration.GetConnectionString("mysql"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))
  );
});

var app = builder.Build();
#endregion

#region UsuarioMaster
using (var scope = app.Services.CreateScope())
{
  var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
  await dbContext.CriarUsuarioMasterAsync();
}
#endregion

#region Home
app.MapGet("/", () => Results.Json(new Home())).WithTags("Home");
#endregion

// #region Login
// app.MapPost("/login", async ([FromBody] LoginDTO loginDTO, ILoginService LoginService) =>
// {
//   if (loginDTO.Email == "email")
//   {
//     // Autenticação de administrador
//     bool loginAdminValido = await LoginService.LoginAdminAsync(loginDTO.Email, loginDTO.Senha);
//     if (loginAdminValido)
//     {
//       return Results.Ok("Login realizado com sucesso!");
//     }
//   }
//   else
//   {
//     // Autenticação de usuário
//     bool loginUsuarioValido = await LoginService.LoginAdminAsync(loginDTO.Email, loginDTO.Senha);
//     if (loginUsuarioValido)
//     {
//       return Results.Ok("Login realizado com sucesso!");
//     }
//   }

//   return Results.Unauthorized();
// }).WithTags("Login");
// #endregion

#region App
if(app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Solution Track v1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion