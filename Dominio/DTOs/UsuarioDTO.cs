using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SolutionTrack.Dominio.Entidades;

namespace SolutionTrack.Dominio.DTOs
{
    public class UsuarioDTO
    {
        public string Nome { get; set; } = default!;     
        public string Email { get; set; } = default!;
        public string Senha { get; set; } = default!;
    }
}