using GopalAPIDemo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GopalAPIDemo.Controllers
{
    public class LoginController : ControllerBase
    {
        IConfiguration Configuration;
        public LoginController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #region Login Authentication
        // GET: api/Login
        [HttpPost]
        [Route("api/login")]
        public IActionResult Authenticate([FromBody] User user)
        {
            if (user.UserName != string.Empty && user.Password != string.Empty)
            {
                string token = CreateToken(user.Password);
                return Ok(token);
                //var userToken = new JwtSecurityToken(Configuration["Jwt:Issuer"], Configuration["Jwt:Issuer"], null);

                //return JsonResult(new
                //{issue
                //    Token = token,
                //    User = user
                //});
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion

        #region Token Generation
        private string CreateToken(string userName)
        {
            JwtSecurityTokenHandler tokenhandler = new JwtSecurityTokenHandler();

            ClaimsIdentity claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name,userName)
            });

            string secret = Configuration["Jwt:Key"];
            DateTime notBefore = DateTime.UtcNow;
            DateTime expiresAfter = DateTime.UtcNow.AddHours(24);
            SecurityKey secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
            SigningCredentials signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var securityToken = tokenhandler.CreateJwtSecurityToken(subject: claims, notBefore: notBefore, expires: expiresAfter, signingCredentials: signingCredentials);

            return tokenhandler.WriteToken(securityToken);
        }
        #endregion
    }
}
