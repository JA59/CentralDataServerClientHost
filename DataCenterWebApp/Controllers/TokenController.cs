using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using iCDataCenterClientHost.CustomIdentity;
using iCDataCenterClientHost.ViewModels.Token;
using Microsoft.Extensions.Logging;

namespace iCDataCenterClientHost.Controllers
{
    public class TokenController : BaseApiController
    {
        #region Private Members
        private readonly ILogger<TokenController> _logger;

        #endregion Private Members

        #region Constructor
        public TokenController(
            RoleManager<DataCenterRole> roleManager,
            UserManager<DataCenterUser> userManager,
            IConfiguration configuration,
            ILogger<TokenController> logger
            ) : base(roleManager, userManager, configuration)
        {
            _logger = logger;
        }
        #endregion

        [HttpPost("Auth")]
        public async Task<IActionResult> Auth([FromBody]TokenRequestVM model)
        {
            _logger.LogInformation("Auth");
            
            // return a generic HTTP Status 500 (Server Error)
            // if the client payload is invalid.
            if (model == null) return new StatusCodeResult(500);

            switch (model.vm_grant_type)
            {
                case "password":
                    return await GetToken(model);
                default:
                    // not supported - return a HTTP 401 (Unauthorized)
                    return new UnauthorizedResult();
            }
        }

        private async Task<IActionResult> GetToken(TokenRequestVM model)
        {
            try
            {
                // check if there's an user with the given username
                DataCenterUser user = await UserManager.FindByNameAsync(model.vm_username);

                if (user == null || !await UserManager.CheckPasswordAsync(user, model.vm_password))
                {
                    // user does not exists or password mismatch
                    return new UnauthorizedResult();
                }

                // username & password matches: create and return the Jwt token.

                DateTime now = DateTime.UtcNow;

                // Add the registered claims for JWT (RFC7519).
                // (For more info, see https://tools.ietf.org/html/rfc7519#section-4.1)
                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat,
                        new DateTimeOffset(now).ToUnixTimeSeconds().ToString())
                };

                var tokenExpirationMins =
                    Configuration.GetValue<int>("Auth:Jwt:TokenExpirationInMinutes");

                var issuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Configuration["Auth:Jwt:Key"]));

                var token = new JwtSecurityToken(
                    issuer: Configuration["Auth:Jwt:Issuer"],
                    audience: Configuration["Auth:Jwt:Audience"],
                    claims: claims,
                    notBefore: now,
                    expires: now.Add(TimeSpan.FromMinutes(tokenExpirationMins)),
                    signingCredentials: new SigningCredentials(
                        issuerSigningKey, SecurityAlgorithms.HmacSha256)
                );
                var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

                // build & return the response
                var response = new TokenResponseVM()
                {
                    vm_token = encodedToken,
                    vm_expiration = tokenExpirationMins,
                    vm_username = user.UserName,
                    vm_isadmin = user.Roles.Contains(DataCenterIdentities.AdminRole)
                };
                return Json(response);
            }
            catch (Exception)
            {
                return new UnauthorizedResult();
            }
        }
    }
}
