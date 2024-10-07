using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolutionTrack.Dominio.DTOs;
using SolutionTrack.Dominio.Entidades;
using SolutionTrack.Dominio.Interfaces;
using SolutionTrack.Dominio.Servicos;
using SolutionTrack.Infraestrutura.Db;

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

using (var scope = app.Services.CreateScope())
{
  var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
  await dbContext.CriarUsuarioMasterAsync();
}

app.MapGet("/", () => "Hello World!");

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
});

app.MapPost("/usuarios", async ([FromBody] Usuario usuario, IUsuarioService usuarioService) =>
  {
    var novoUsuario = await usuarioService.CriarUsuario(usuario);
    return Results.Created($"/usuarios/{novoUsuario.Id}", novoUsuario);
  });

app.MapGet("/usuarios/{id:int}", async (int id, IUsuarioService usuarioService) =>
{
  var usuario = await usuarioService.ObterUsuarioPorId(id);
  return Results.Ok(usuario);
});

app.MapGet("/usuarios", async (IUsuarioService usuarioService, int pagina = 1, int tamanhoPagina = 10) =>
{
  var (usuarios, total) = await usuarioService.ObterTodosUsuarios(pagina, tamanhoPagina);
  return Results.Ok(new { Total = total, Usuarios = usuarios });
});

app.MapPut("/usuarios/id", async (int id, Usuario usuario, IUsuarioService usuarioService) =>
{
  var usuarioExistente = await usuarioService.ObterUsuarioPorId(id);

  if (usuarioExistente == null)
  {
    return Results.NotFound();
  }

  usuario.Id = id;
  var usuarioAtualizado = await usuarioService.AtualizarUsuario(usuario);
  return Results.Ok(usuarioAtualizado);
});

app.MapDelete("/usuarios/id", async (int id, IUsuarioService usuarioService) =>
{
  var sucesso = await usuarioService.ApagarUsuario(id);

  if (!sucesso)
  {
    return Results.NotFound();
  }

  return Results.NoContent();
});

app.UseSwagger();
app.UseSwaggerUI();
app.Run();
