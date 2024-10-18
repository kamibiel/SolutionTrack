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
        [StringLength(255)]
        public string Email { get; set; } = default!;

        [StringLength(60)]
        public string Senha { get; set; } = default!;
    }
}