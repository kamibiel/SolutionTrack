using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionTrack.Dominio.Configuracoes
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public int ExpirationMinutes { get; set; }
    }
}