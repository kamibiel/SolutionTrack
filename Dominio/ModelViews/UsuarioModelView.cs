using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionTrack.Dominio.ModelViews
{
    public record UsuarioModelView
    {
        public int Id { get; set; } = default;
        public string Nome { get; set; } = default!;        
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public int PerfilId { get; set; }
    }
}