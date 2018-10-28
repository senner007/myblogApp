using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyblogApp.Models;

namespace MyblogApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class LoginController : ControllerBase
    {
       
        [HttpPost("token")]
        public async Task<IActionResult> Token() 
        {

            // from https://www.youtube.com/watch?v=RRgBhgrBpKU&index=2&list=PLgWtWADUClwBhw1EIHtxAWBgUQ_Esc7c4
            var header = Request.Headers["Authorization"];
            if (!header.ToString().StartsWith("Basic")) return BadRequest("Authorization Header should start with 'Basic");

            string credValue = header.ToString().Substring("Basic ".Length).Trim();
            string usernameAndPassenc = Encoding.UTF8.GetString(Convert.FromBase64String(credValue));
            string[] usernameAndPass = usernameAndPassenc.Split(":");
            string username = usernameAndPass[0];
            string password = usernameAndPass[1];

            // check in db
            // username : Admin, password : pass
            // username : User, password : pass2

            ValidateUser validateUser = new ValidateUser();

            // TODO: Er det nødvendigt at eksekvere det async?
            // https://stackoverflow.com/questions/31185072/effectively-use-async-await-with-asp-net-web-api
            if ( await validateUser.CheckUserAndPassword(username, password)) 
            {
                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettingsClass.SecurityKey));
                
                JwtSecurityToken token = new JwtSecurityToken(
                        issuer: "https://localhost:5001/",
                        audience: "mysite.com",
                        expires: DateTime.Now.AddMinutes(100),
                        claims: new[] { new Claim(ClaimTypes.Name, username)},
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

                string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(tokenString);
            }
            return BadRequest("Wrong Username and/or Password!");
        
        }

    }
    class ValidateUser 
    {
        MyblogContext dbContext = new MyblogContext();
        public string GetUserName(string user) => dbContext.Users.Where(w => w.Username == user).FirstOrDefault()?.Username;
        public string GetUserPass(string user) => dbContext.Users.Where(w => w.Username == user).Single().Password;

        public async Task<bool> CheckUserAndPassword (string user, string password) 
        {
            if (String.IsNullOrEmpty(user) || String.IsNullOrEmpty(password)) {  
                return false;
            }
            if (GetUserName(user) != null) {
             
                if (await Task.Factory.StartNew(() => CalculateHash(user, password))) return true;
                return false;
            }
             // Dette er for at eksekvere argon2 algoritmen uanset brugernavn. Responstiden forbliver derved ens.
            await Task.Factory.StartNew(() => CalculatePasswordFirst());
            return false;
        }
        private void CalculatePasswordFirst() 
        {
            Argon2.Verify(dbContext.Users.Where(w => w.Id == 1).First().Password, "dummy");
        }
        private bool CalculateHash(string user, string password) 
        {
            return Argon2.Verify(GetUserPass(user), password);
        }
    }
    
}
