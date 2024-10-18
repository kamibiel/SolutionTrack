using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SolutionTrack.Dominio.Configuracoes;
using SolutionTrack.Dominio.Entidades;

namespace SolutionTrack.Dominio.Servicos
{
    public class JwtService
    {
        private readonly JwtConfig _jwtConfig;

        public JwtService(IOptions<JwtConfig> jwtConfig)
        {
            _jwtConfig = jwtConfig.Value;
        }

        public string GenerateToken(Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Email))
            {
                throw new ArgumentNullException(nameof(usuario.Email), "O email do usuário não pode ser nulo.");
            }

            var claims = new[]
            {
                new Claim("Email", usuario.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfig.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtConfig.ExpirationMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}