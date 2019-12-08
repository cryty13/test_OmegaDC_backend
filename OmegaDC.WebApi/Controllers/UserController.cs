using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OmegaDC.Domain.Entities;
using OmegaDC.Domain.Repositories;
using OmegaDC.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmegaDC.WebApi.Controllers
{
    public class UserController : Controller , IUserRepositoryController
    {
        private readonly IUserRepository _repository;
        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("Get/User")]
        public Task<List<User>> Get()
        {
            return _repository.Get();
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("Save/User")]
        public Task<User> Save([FromBody]object obj)
        {
            User user = JsonConvert.DeserializeObject<User>(obj.ToString());
            return _repository.Save(user);
        }
    }
}
