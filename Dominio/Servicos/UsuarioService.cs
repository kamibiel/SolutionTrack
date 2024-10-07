using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SolutionTrack.Dominio.Entidades;
using SolutionTrack.Dominio.Interfaces;
using SolutionTrack.Infraestrutura.Db;

namespace SolutionTrack.Dominio.Servicos
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ApplicationDbContext _application;
        public UsuarioService (ApplicationDbContext application)
        {
            _application = application;
        }
        public async Task<bool> ApagarUsuario(int id)
        {
            var usuario = await _application.Usuarios.FindAsync(id);
            if(usuario == null) return false;

            _application.Usuarios.Remove(usuario);
            await _application.SaveChangesAsync();
            return true;
        }

        public async Task<Usuario> AtualizarUsuario(Usuario usuario)
        {
            _application.Usuarios.Update(usuario);
            await _application.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> CriarUsuario(Usuario usuario)
        {
            _application.Usuarios.Add(usuario);
            await _application.SaveChangesAsync();
            return usuario;
        }

        public async Task<(IEnumerable<Usuario> Usuarios, int TotalUsuarios)> ObterTodosUsuarios(
            int pagina = 1, 
            int tamanhoPagina = 10,
            string? nome = null, 
            string? username = null, 
            string? email = null)
        {
            var query = _application.Usuarios.AsQueryable();

            if(!string.IsNullOrEmpty(nome))
            {
                query = query.Where(u => EF.Functions.Like(u.Nome.ToLower(), $"%{nome.ToLower()}%"));
            }

            if(!string.IsNullOrEmpty(username))
            {
                query = query.Where(u => EF.Functions.Like(u.Username.ToLower(), $"%{username.ToLower()}%"));
            }

            if(!string.IsNullOrEmpty(email))
            {
                query = query.Where(u => EF.Functions.Like(u.Email.ToLower(), $"%{email.ToLower()}%"));
            }

            // Obter o total de usuários filtrados
            var totalUsuarios = await query.CountAsync();

            // Paginação
            var usuarios = await query
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();

            // Retornar os usuários paginados e o total
            return(usuarios, totalUsuarios);
        }

        public async Task<Usuario> ObterUsuarioPorId(int id)
        {
            return await _application.Usuarios.FindAsync(id);
        }
    }
}