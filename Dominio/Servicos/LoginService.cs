using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SolutionTrack.Dominio.Interfaces;
using SolutionTrack.Infraestrutura.Db;

namespace SolutionTrack.Dominio.Servicos
{
    public class LoginService : ILoginService
    {
        private readonly ApplicationDbContext _dbContext;

        public LoginService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> LoginAdminAsync(string usernameOrEmail, string senha)
        {
            // Busca o administrador no banco de dados
            var admin = await _dbContext.Usuarios
                .FirstOrDefaultAsync(u => u.Email == usernameOrEmail);
            
            if(admin == null) return false;

            return BCrypt.Net.BCrypt.Verify(senha, admin.Senha);
        }

        public async Task<bool> LoginUsuarioAsync(string usernameOrEmail, string senha)
        {
            // Busca o usuário no banco de dados
            var usuario = await _dbContext.Usuarios
                .FirstOrDefaultAsync(u => u.Email == usernameOrEmail);
            
            if(usuario == null) return false;

            return BCrypt.Net.BCrypt.Verify(senha, usuario.Senha);
        }
    }
}