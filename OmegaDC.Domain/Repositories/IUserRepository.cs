using OmegaDC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OmegaDC.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> Save(User user);
        Task<List<User>> Get();
    }
}
