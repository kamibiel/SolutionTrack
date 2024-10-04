using SolutionTrack.Dominio.DTOs;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/login", (LoginDTO loginDTO) => 
{
  if((loginDTO.Email == "gabriel.o.bonifacio@gmail.com" && loginDTO.Senha == "ASDzxc456!@#*") || 
    (loginDTO.Username == "kamibiel" && loginDTO.Senha == "ASDzxc456!@#*"))
  {
    return Results.Ok("Login com sucesso");
  }
  else
  {
    return Results.Unauthorized();
  }
});

app.Run();
