using Dapper;
using OmegaDC.Domain.Entities;
using OmegaDC.Domain.Repositories;
using OmegaDC.Infra.StoreContext.DataContexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OmegaDC.Infra.StoreContext.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly OmegaDcDataContext _context;
        public UserRepository(OmegaDcDataContext context)
        {
            _context = context;
        }

        Task<List<User>> IUserRepository.Get()
        {
            return Task.FromResult(GetAll());
        }
        public List<User> GetAll()
        {
            return
                 _context.Connection.Query<User>("User_GetAll", commandType: CommandType.StoredProcedure).AsList();
        }

        public Task<User> Save(User User)
        {

            User.Password = Sha256(User.Password);
            _context.Connection.Execute("User_Create",
             new
             {
                 Name = User.Name,
                 Password = User.Password,
                 Email = User.Email,

             }, commandType: CommandType.StoredProcedure);

            return Task.FromResult<User>(User);
        }
        private static string Sha256(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
