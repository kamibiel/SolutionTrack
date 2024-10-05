using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using SolutionTrack.Dominio.Entidades;

namespace SolutionTrack.Infraestrutura.Db
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuracaoAppSettings;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuracaoAppSettings)
            : base(options)
        {
            _configuracaoAppSettings = configuracaoAppSettings;
        }
        public DbSet<Usuario> Usuarios { get; set; } = default!;
        public DbSet<Perfil> Profiles { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                var stringConexao = _configuracaoAppSettings.GetConnectionString("mysql")?.ToString();
                if (!string.IsNullOrEmpty(stringConexao))
                {
                    optionsBuilder.UseMySql(
                        stringConexao,
                        ServerVersion.AutoDetect(stringConexao)
                    );
                }
            }
        }

        public async Task CriarUsuarioMasterAsync()
        {
            var usuarioMasterEmail = "usuario@example.com";
            var usuarioMasterNome = "Usuário Master";
            var usuarioMasterUsername = "usuario";
            var perfilAdmin = new Perfil { Nome = "Master" };

            var usuarioMaster = await Usuarios
                .Where(u => u.Email == usuarioMasterEmail || u.Username == usuarioMasterUsername)
                .FirstOrDefaultAsync();

            if (usuarioMaster == null)
            {
                var hashedSenha = BCrypt.Net.BCrypt.HashPassword("Senha");

                var novoUsuarioMaster = new Usuario
                {
                    Email = usuarioMasterEmail,
                    Nome = usuarioMasterNome,
                    Username = usuarioMasterUsername,
                    Senha = hashedSenha,
                    Perfil = perfilAdmin
                };

                Usuarios.Add(novoUsuarioMaster);
                try
                {
                    await SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}