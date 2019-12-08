using System;
using System.Collections.Generic;
using System.Text;

namespace OmegaDC.Domain.Entities
{
    public class Login
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
