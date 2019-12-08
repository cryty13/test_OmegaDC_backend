using System;
using System.Collections.Generic;
using System.Text;

namespace OmegaDC.Domain.Entities
{
    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime DtCriacao { get; set; }
        public bool Ativo { get; set; }
    }
}
