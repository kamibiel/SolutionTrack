using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionTrack.Dominio.Entidades
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = default;

        [Required]
        [StringLength(255)]
        public string Nome { get; set; } = default!;

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = default!;

        [Required]
        [StringLength(255)]
        public string Email { get; set; } = default!;

        // [Required]
        // [StringLength(20)]
        // public string Cpf { get; set; } = default!;

        [StringLength(60)]
        public string Senha { get; set; } = default!;

        [ForeignKey("Perfil")]
        public int PerfilId { get; set; }

        [StringLength(10)]
        public Perfil Perfil { get; set; } = default!;
    }
}