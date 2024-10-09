using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolutionTrack.Dominio.DTOs;
using SolutionTrack.Dominio.Entidades;
using SolutionTrack.Dominio.Interfaces;
using SolutionTrack.Dominio.ModelViews;
using SolutionTrack.Dominio.Servicos;
using SolutionTrack.Infraestrutura.Db;

#region Builder
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

#region Usuários
app.MapPost("/usuarios", async ([FromBody] UsuarioDTO usuarioDTO, IUsuarioService usuarioService) =>
  {
    var novoUsuario = await usuarioService.CriarUsuario(usuarioDTO);
    return Results.Created($"/usuarios", novoUsuario);
  }).WithTags("Usuários");

app.MapGet("/usuarios/{id:int}", async (int id, IUsuarioService usuarioService) =>
{
  var usuario = await usuarioService.ObterUsuarioPorId(id);
  return Results.Ok(usuario);
}).WithTags("Usuários");

app.MapGet("/usuarios", async (IUsuarioService usuarioService, int pagina = 1, int tamanhoPagina = 10) =>
{
  var (usuarios, total) = await usuarioService.ObterTodosUsuarios(pagina, tamanhoPagina);
  return Results.Ok(new { Total = total, Usuarios = usuarios });
}).WithTags("Usuários");

app.MapPut("/usuarios/{id}", async ([FromRoute] int id, UsuarioDTO usuarioDTO, IUsuarioService usuarioService) =>
{
  var usuario = await usuarioService.ObterUsuarioPorId(id);
  if(usuario == null) return Results.NotFound("Usuário não existe.");

  usuario.Nome = usuarioDTO.Nome;
  usuario.Username = usuarioDTO.Username;
  usuario.Email = usuarioDTO.Email;
  usuario.Senha = usuarioDTO.Senha;
  usuario.PerfilId = usuarioDTO.PerfilId;

  var usuarioAtualizado = await usuarioService.AtualizarUsuario(usuario);
  return Results.Ok(usuarioAtualizado);
}).WithTags("Usuários");

app.MapDelete("/usuarios/{id}", async ([FromRoute] int id, IUsuarioService usuarioService) =>
{
  var usuario = await usuarioService.ObterUsuarioPorId(id);
  if(usuario == null) return Results.NotFound("Usuário não existe.");
  
  await usuarioService.ApagarUsuario(id);
    return Results.NoContent();
}).WithTags("Usuários");
#endregion

#region App
app.UseSwagger();
app.UseSwaggerUI();
app.Run();
#endregion