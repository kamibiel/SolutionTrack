using Microsoft.EntityFrameworkCore;
using SolutionTrack.Dominio.DTOs;
using SolutionTrack.Infraestrutura.Db;

var builder = WebApplication.CreateBuilder(args);

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

app.MapPost("/login", async (LoginDTO loginDTO, ApplicationDbContext dbContext) =>
{
    // Busca o usuario pelo email ou username
    var usuario = await dbContext.Usuarios
      .FirstOrDefaultAsync(a => a.Email == loginDTO.Email || a.Username == loginDTO.Username);

    // Verifica se o usuario foi encontrado e se a senha está correta
    if (usuario != null)
    {
        bool senhaValida = BCrypt.Net.BCrypt.Verify(loginDTO.Senha, usuario.Senha);
        
        if (senhaValida) // Verifica se a senha é válida
        {
            return Results.Ok("Login com sucesso!");
        }
    }

    // Verifica se está tentando fazer login como o usuário Master
    var usuarioMaster = await dbContext.Usuarios
      .FirstOrDefaultAsync(u => u.Username == "kamibiel");

    if(usuarioMaster != null)
    {
      bool senhaMasterValida = BCrypt.Net.BCrypt.Verify(loginDTO.Senha, usuarioMaster.Senha);

      if(loginDTO.Username == "kamibiel" && senhaMasterValida)
      {
        return Results.Ok("Login com sucesso!");
      }
    }
    
    return Results.Unauthorized();
});

app.Run();
