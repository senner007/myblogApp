using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.Serialization;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using MyblogApp.Models;


// {Lav en REST-baseret webservice, som kan håndtere opskrifter. 
// En opskrift består af et navn, en beskrivelse og en række ingredienser.
// Lav som minimum implementeringen af GET (som returnerer navnene på alle opskrifter) og GET/id som returnerer en specifik opskrift.
// Lav også gerne POST som tilføjer en opskrift til samlingen.
namespace webapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        MyblogContext dbContext = new MyblogContext();
        string ToLowerOREmpty(string query) => !String.IsNullOrEmpty(query) ? query.ToLower() : "";
        // GET api/values
        [HttpGet]
        public IEnumerable<Recipies> Get(string queryTitle, string queryIngredient)
        {          
            return dbContext.Recipies.Where(w => w.Title.ToLower().Contains(ToLowerOREmpty(queryTitle)) && w.Ingredients.ToLower().Contains(ToLowerOREmpty(queryIngredient)));  
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Recipies recipe = dbContext.Recipies.Where(w => w.Id == id).FirstOrDefault(); 
            
            if (recipe == null) {
                return NotFound($"The recipe with the following id: {id} was not found");
            }
            return Ok(recipe);
        }

        // POST api/values
        [HttpPost]
        public bool Post([FromBody] Recipies recipe)
        {
            // string pattern = @"^[a-zæøåA-ZÆØÅ\d]+$";
            // List<bool> validationList = new List<bool>() {
            //     Regex.Match(recipe.Title, pattern).Success,
            //     Regex.Match(recipe.Description, pattern).Success,
            //     !recipe.Ingredients.Any(i => !Regex.Match(i, pattern).Success)
            // };
            // if (validationList.Any(a => !a)) return false; 

            // TODO : VALIDATE ME!  
            dbContext.Recipies.Add(new Recipies() { Title = recipe.Title, Description = recipe.Description, Ingredients = recipe.Ingredients });
            dbContext.SaveChanges();
            return true;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

}
