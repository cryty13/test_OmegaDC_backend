using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OmegaDC.Domain.Entities;
using OmegaDC.Domain.Repositories;
using OmegaDC.WebApi.Repositories;
using OmegaDC.WebApi.Shared.Token;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace OmegaDC.WebApi.Controllers
{
    public class LoginController : Controller, ILoginRepositoryController
    {
        private readonly ILoginRepository _repository;
        private readonly TokenOptions _tokenOptions;
        private readonly JsonSerializerSettings _serializerSettings;

        public LoginController(ILoginRepository repository, IOptions<TokenOptions> jwtOptions)
        {
            _repository = repository;
            _tokenOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_tokenOptions);

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }
        [HttpGet]
        [Route("Get/Login")]
        public IEnumerable<Login> Get()
        {
            return _repository.Get();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<string> Login([FromBody]object obj)
        {
            Login login = JsonConvert.DeserializeObject<Login>(obj.ToString());

            login = _repository.ValidateLogin(login);
            if (login?.UserId != null)
            {
                var identity = await GetClaims(login);
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, login.Email),
                    new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_tokenOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                    identity.FindFirst("OmegaDc"),
                };

                var jwt = new JwtSecurityToken(
                    issuer: _tokenOptions.Issuer,
                    audience: _tokenOptions.Audience,
                    claims: claims.AsEnumerable(),
                    notBefore: _tokenOptions.NotBefore,
                    expires: _tokenOptions.Expiration,
                    signingCredentials: _tokenOptions.SigningCredentials);

                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                var response = new
                {
                    token = encodedJwt,
                    expires = _tokenOptions.Expiration,
                    user = new
                    {
                        id = login.UserId,
                        email = login.Email,
                    }
                };
                var json = JsonConvert.SerializeObject(response, _serializerSettings);
                return json;
            }
            else
                return null;
        }
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        public Task<ClaimsIdentity> GetClaims(Login Login)
        {
            return Task.FromResult(
                new ClaimsIdentity(
                    new GenericIdentity(Login.Email, "Token"),
                    new[] {
                        new Claim("OmegaDc", "User"),
                        new Claim("ID",Convert.ToString(Login.UserId))
                 }));
        }
        private static void ThrowIfInvalidOptions(TokenOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.SigningCredentials == null)
                throw new ArgumentNullException(nameof(TokenOptions.SigningCredentials));

            if (options.JtiGenerator == null)
                throw new ArgumentNullException(nameof(TokenOptions.JtiGenerator));
        }
    }
}
