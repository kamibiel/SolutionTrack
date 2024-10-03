using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SolutionTrack.Context;
using SolutionTrack.Entities;

namespace SolutionTrack.Controllers
{
    [ApiController]
        [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly SolutionTrackContext _context;
        public UsuarioController(SolutionTrackContext context)
        {
            _context = context;
        }
        // Cadastra usuário
        [HttpPost]
        public IActionResult Create(Usuario usuario)
        {   
            try
            {   
                // Adiciona o usuário
                var novoUserEntry = _context.Add(usuario);
                // Salva as alterações no banco de dados
                _context.SaveChanges();
                // Acessa o objeto recém-adicionado
                var novoUser = novoUserEntry.Entity;

                // Retornar os campos Nome, Username, Cpf e Email
                return Ok(new {
                    novoUser.Nome,
                    novoUser.Username,
                    novoUser.CPF,
                    novoUser.Email
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    errors = new[] { e.Message }
                });
            }
        }
        // Consulta todos os usuário
        [HttpGet("ObterTodosUsuarios")]
        public IActionResult ObterTodosUsuarios()
        {
            try
            {
                // Busca todos os usuários
                var usuarios = _context.Usuarios;

                // Verifica se existe usuários
                if(!usuarios.Any())
                {
                    return NotFound("Nenhum usuário encontrado.");
                }
                // Seleciona os campos desejados
                var resultado = usuarios.Select(u => new
                {
                    u.Nome,
                    u.Username,
                    u.CPF,
                    u.Email
                }).ToList();
                // Retorna a lista dos usuários
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    errors = new [] { e.Message }
                });
            }
        }
        // Consulta o usuário por nome, username, cpf ou email
        [HttpGet("ObterUsuario")]
        public IActionResult ObterUsuario(string users)
        // public IActionResult ObterDados(string? nome, string? username, string? cpf, string? email)
        {
            try
            {
                // Busca Usuarios por nome, username, cpf ou email
               
                var usuarios = _context.Usuarios.Where(u =>
                    (users == null ||
                    u.Nome.Contains(users) ||
                    u.Username.Contains(users) ||
                    u.CPF.Contains(users) ||
                    u.Email.Contains(users))
                );

                if(!usuarios.Any())
                {
                    return NotFound("Nenhum usuário encontrado.");
                }

                // Execulta a consulta e transforma o reusltado em uma lista
                var resultado = usuarios.Select(u => new
                {
                    u.Nome,
                    u.Username,
                    u.CPF,
                    u.Email
                }).ToList();
             

                return Ok(resultado);
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    errors = new [] { e.Message }
                });
            }
        }
        // Atualiza o usuário
        [HttpPut("{id}")]
        public IActionResult AtualizarUsuario(int id, Usuario usuario)
       { 
            try
            {
                // Busca um usuário
                var usuarioExistente = _context.Usuarios.Find(idy);

                if(usuarioExistente == null)
                {
                    return NotFound("Nenhum usuário encontrado.");
                }

                // var resultado = usuarioExistente.Select(u => new
                // {
                //     u.Nome, 
                //     u.Username,
                //     u.CPF,
                //     u.Email
                // }).ToList();

                usuarioExistente.Nome = usuario.Nome;
                usuarioExistente.Username = usuario.Username;
                usuarioExistente.CPF = usuario.CPF;
                usuarioExistente.Email = usuario.Email;

                _context.Usuarios.Update(usuarioExistente);
                _context.SaveChanges();

                return Ok(usuarioExistente);

            }
             catch (Exception e)
            {
                return StatusCode(500, new
                {
                    errors = new [] { e.Message }
                });
            }
        }
    }
}