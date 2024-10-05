using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionTrack.Dominio.Entidades
{
    public class Perfil
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = default;

        [Required]
        [StringLength(50)]
        public string Nome { get; set; } = default!;
    }
}