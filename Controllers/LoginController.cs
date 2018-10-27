using System;
//using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Isopoh.Cryptography.Argon2;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Cors;
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
        public IActionResult Token() 
        {

            // from https://www.youtube.com/watch?v=RRgBhgrBpKU&index=2&list=PLgWtWADUClwBhw1EIHtxAWBgUQ_Esc7c4
            var header = Request.Headers["Authorization"];
            if (header.ToString().StartsWith("Basic"))
            {
                String credValue = header.ToString().Substring("Basic ".Length).Trim();
                String usernameAndPassenc = Encoding.UTF8.GetString(Convert.FromBase64String(credValue));
                String[] usernameAndPass = usernameAndPassenc.Split(":");
                string username = usernameAndPass[0];
                string password = usernameAndPass[1];

                
                // check in db
                // username : Admin, password : pass
                // username : User, password : pass2
                
                ValidateUser validateUser = new ValidateUser();
                if ( validateUser.CheckUserAndPassword(username, password)) 
                {
                    SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettingsClass.SecurityKey));
                    
                    JwtSecurityToken token = new JwtSecurityToken(
                         issuer: "https://localhost:5001/",
                         audience: "mysite.com",
                         expires: DateTime.Now.AddMinutes(100),
                         claims: new[] { new Claim(ClaimTypes.Name, usernameAndPass[0])},
                        signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                    );

                    String tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                    return Ok(tokenString);
                }
                return BadRequest("Wrong Username and/or Password!");
            }
            return BadRequest("Wrong request!");
        }




        // MyblogContext dbContext = new MyblogContext();
        // // Det rigtige sted at indlæse dbContext ? 
        
        // [HttpGet]
        // public ActionResult<IEnumerable<Posts>> Get()
        // {
        //     return dbContext.Posts;
        // }

        // // POST api/values
        // [HttpPost]
        // public void Post([FromBody] string value)
        // {
        // }

        // // Update to PBKDF2 : https://stackoverflow.com/questions/4181198/how-to-hash-a-password/10402129#10402129
        // public static String sha256_hash(string value)
        // {
        //     StringBuilder Sb = new StringBuilder();

        //     using (var hash = SHA256.Create())            
        //     {
        //         Encoding enc = Encoding.UTF8;
        //         Byte[] result = hash.ComputeHash(enc.GetBytes(value));

        //         foreach (Byte b in result)
        //             Sb.Append(b.ToString("x2"));
        //     }

        //     return Sb.ToString();
        // }
        // public static String argon2_hash(string value)
        // {
        //     return Argon2.Hash(value);
        // }

    }
    public class ValidateUser 
    {
        MyblogContext dbContext = new MyblogContext();
        public string GetUserName(string user) => dbContext.Users.Where(w => w.Username == user).FirstOrDefault()?.Username;
        public string GetUserPass(string user) => dbContext.Users.Where(w => w.Username == user).Single().Password;

        public bool CheckUserAndPassword (string user, string password) 
        {
            if (String.IsNullOrEmpty(user) || String.IsNullOrEmpty(password)) {  
                return false;
            }
            if (GetUserName(user) != null) {
                if (Argon2.Verify(GetUserPass(user), password)) return true;
                return false;
            }
             // Dette er for at eksekvere argon2 hashalgoritmen uanset brugernavn. Responstiden forbliver derved ens.
            CalculatePasswordFirst();
            return false;
        }
        public void CalculatePasswordFirst() 
        {
            Argon2.Verify(dbContext.Users.Where(w => w.Id == 1).First().Password, "dummy");
        }
    }

    
}
