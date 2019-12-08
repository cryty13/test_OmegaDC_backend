using OmegaDC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OmegaDC.Domain.Repositories
{
    public interface ILoginRepository
    {
        IEnumerable<Login> Get();
        Task<string> Login(Login login);
        Login ValidateLogin(Login login);
    }
}
