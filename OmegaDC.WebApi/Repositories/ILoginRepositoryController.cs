using OmegaDC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OmegaDC.WebApi.Repositories
{
    public interface ILoginRepositoryController
    {
        IEnumerable<Login> Get();
        Task<string> Login(object obj);
    }
}
