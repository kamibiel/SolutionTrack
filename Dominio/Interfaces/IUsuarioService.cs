using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SolutionTrack.Dominio.DTOs;
using SolutionTrack.Dominio.Entidades;

namespace SolutionTrack.Dominio.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO> CriarUsuario(UsuarioDTO usuarioDTO);
        Task<Usuario> AtualizarUsuario(Usuario usuario);
        Task<bool> ApagarUsuario(int id);
        Task<Usuario> ObterUsuarioPorId(int id);
        Task<(IEnumerable<Usuario> Usuarios, int TotalUsuarios)> ObterTodosUsuarios(int pagina = 1,
            int tamanhoPagina = 10, string? nome = null, string? username = null, string? email = null);
    }
}