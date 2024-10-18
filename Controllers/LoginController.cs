using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SolutionTrack.Dominio.DTOs;
using SolutionTrack.Dominio.Servicos;
using SolutionTrack.Dominio.Interfaces;
using SolutionTrack.Dominio.Entidades;

namespace SolutionTrack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Tags("Login")]
    public class LoginController: ControllerBase
    {   
        private readonly JwtService _jwtService;
        private readonly ILoginService _loginService;
        public LoginController(JwtService jwtService, ILoginService loginService)
        {
            _loginService = loginService;
            _jwtService = jwtService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if(string.IsNullOrEmpty(loginDTO.Email))
            {
                return BadRequest("Email não pode estar vazio.");
            }            

            if(loginDTO.Email == "gabriel.o.bonifacio@gmail.com")
            {
                bool loginAdminValido = await _loginService.LoginAdminAsync(loginDTO.Email, loginDTO.Senha);
                if(loginAdminValido)
                {
                    var token = _jwtService.GenerateToken(new Usuario
                    {
                        Email = loginDTO.Email
                    });
                    return Ok(new 
                    {
                        token = token,
                        message = "Login realizado com sucesso!"
                    });
                }
            }
            else
            {
                bool loginUsuarioValido = await _loginService.LoginUsuarioAsync(loginDTO.Email, loginDTO.Senha);
                if(loginUsuarioValido)
                {
                    var token = _jwtService.GenerateToken(new Usuario 
                    {
                        Email = loginDTO.Email
                    });
                    return Ok(new
                    {
                        token = token,
                        message = "Login realizado com sucesso!"
                    });
                }
            }

            return Unauthorized("Email ou senha inválido.");
        }
    }
}