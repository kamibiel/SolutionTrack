using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SolutionTrack.Dominio.DTOs;
using SolutionTrack.Dominio.Entidades;
using SolutionTrack.Dominio.Interfaces;
using SolutionTrack.Dominio.ModelViews;

namespace SolutionTrack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Usuários")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UsuariosController(IUsuarioService usuarioService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            usuarioDTO.Senha =  BCrypt.Net.BCrypt.HashPassword(usuarioDTO.Senha);

            var usuario = _mapper.Map<Usuario>(usuarioDTO);

            var novoUsuario = await _usuarioService.CriarUsuario(usuarioDTO);
            var usuarioModelView = _mapper.Map<UsuarioModelView>(novoUsuario);
            
            return CreatedAtAction(nameof(ObterUsuarioPorId), new {
                id = usuarioModelView.Id
            }, usuarioModelView);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterUsuarioPorId(int id)
        {
            var usuario = await _usuarioService.ObterUsuarioPorId(id);
            
            if(usuario == null)
                return NotFound("Usuário não existe.");
            
            var usuarioModelView = _mapper.Map<UsuarioModelView>(usuario);
            return Ok(usuarioModelView);
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodosUsuarios([FromQuery] int pagina = 1, [FromQuery] int tamanhoPagina = 10)
        {
            var (usuarios, total) = await _usuarioService.ObterTodosUsuarios(pagina, tamanhoPagina);

            var usuariosModelView = _mapper.Map<List<UsuarioModelView>>(usuarios);

            return Ok(new {
                Total = total,
                Usuarios = usuariosModelView
            });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> AtualizarUsuario(int id, [FromBody] UsuarioDTO usuarioDTO)
        {
            var usuario = await _usuarioService.ObterUsuarioPorId(id);
            if(usuario == null)
            {
                return NotFound("Usuário não existe.");
            }

            if(usuarioDTO.Nome != null)
            {
                usuario.Nome = usuarioDTO.Nome;
            }

            if(usuarioDTO.Username != null)
            {
                usuario.Username = usuarioDTO.Username;
            }

            if(usuarioDTO.Email != null)
            {
                usuario.Email = usuarioDTO.Email;
            }

            if(usuarioDTO.Senha != null)
            {
                usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuarioDTO.Senha);
            }

            if(usuarioDTO.PerfilId.HasValue)
            {
                usuario.PerfilId = usuarioDTO.PerfilId.Value;
            }

            var usuarioAtualizado = await _usuarioService.AtualizarUsuario(usuario);

            var usuarioModelView = _mapper.Map<UsuarioModelView>(usuarioAtualizado);
            return Ok(usuarioModelView);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> ApagarUsuario(int id){
            var usuario = await _usuarioService.ObterUsuarioPorId(id);
            if(usuario == null)
            {
                return NotFound("Usuário não existe.");
            }
            await _usuarioService.ApagarUsuario(id);
            return Ok("Usuário excluído com sucesso.");
        }

    }
}