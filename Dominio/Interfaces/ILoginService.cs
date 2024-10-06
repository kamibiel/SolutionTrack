using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionTrack.Dominio.Interfaces
{
    public interface ILoginService
    {
        Task<bool> LoginAdminAsync(string usernameOrEmail, string senha);
        Task<bool> LoginUsuarioAsync(string usernameOrEmail, string senha);
    }
}