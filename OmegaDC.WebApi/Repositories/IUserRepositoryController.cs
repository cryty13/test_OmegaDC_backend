using OmegaDC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OmegaDC.WebApi.Repositories
{
    public interface IUserRepositoryController
    {
        Task<List<User>> Get();

        Task<User> Save(object obj);
    }
}
