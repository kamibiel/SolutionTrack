using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolutionTrack.Dominio.DTOs;
using SolutionTrack.Dominio.Entidades;
using SolutionTrack.Dominio.Interfaces;
using SolutionTrack.Dominio.ModelViews;
using SolutionTrack.Dominio.Servicos;
using SolutionTrack.Infraestrutura.Db;
using SolutionTrack.Infraestrutura.Mappings;

#region Builder
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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

#region Login
app.MapPost("/login", async ([FromBody] LoginDTO loginDTO, ILoginService LoginService) =>
{
  if (loginDTO.Username == "usuario")
  {
    // Autenticação de administrador
    bool loginAdminValido = await LoginService.LoginAdminAsync(loginDTO.Username, loginDTO.Senha);
    if (loginAdminValido)
    {
      return Results.Ok("Login realizado com sucesso!");
    }
  }
  else
  {
    // Autenticação de usuário
    bool loginUsuarioValido = await LoginService.LoginAdminAsync(loginDTO.Username, loginDTO.Senha);
    if (loginUsuarioValido)
    {
      return Results.Ok("Login realizado com sucesso!");
    }
  }

  return Results.Unauthorized();
}).WithTags("Login");
#endregion

#region App
if(app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion