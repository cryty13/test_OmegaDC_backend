using Dapper;
using OmegaDC.Domain.Entities;
using OmegaDC.Domain.Repositories;
using OmegaDC.Infra.StoreContext.DataContexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OmegaDC.Infra.StoreContext.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly OmegaDcDataContext _context;
        public LoginRepository(OmegaDcDataContext context)
        {
            _context = context;
        }
        public IEnumerable<Login> Get()
        {
            return
                _context
                .Connection
                .Query<Login>("SELECT [LoginId], [PizzaId], [Descricao], [Tempo], [Valor], [QtdAdicional] FROM [Login]", new { });
        }
        public Login ValidateLogin(Login login)
        {
            login.Password = Sha256(login.Password);
            return
                _context.Connection.Query<Login>(
                    "Login_Validate",
                    new
                    {
                        Email = login.Email,
                        Password = login.Password
                    },
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
        }
        public Task<string> Login(Login login)
        {
            throw new NotImplementedException();
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
