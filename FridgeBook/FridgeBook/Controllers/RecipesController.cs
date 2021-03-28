using FridgeBook.Data;
using FridgeBook.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FridgeBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly MongoDbCRUD _context;

        public RecipesController()
        {
            _context = new MongoDbCRUD("Recipes"); // to trzeba zmienic na di
        }

        [HttpPost]
        public ActionResult<Pizza> AddRecipeIntoDatabase() 
        {
            Pizza record = new Pizza
            {
                MustHaveIngredients = new List<string> { "Flour", "Yeast", "Cheese" }
            };

             _context.InsertRecord<Pizza>("Pizza", record);
            return Ok();
        }

        /// <summary>
        /// Function retrives the recipe according to the users requirments 
        /// </summary>
        /// <param name="first">First ingredient</param>
        /// <param name="second">Second ingredient</param>
        /// <param name="third">Third ingredient</param>
        /// <returns>Returns an objects that meets the requirments</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Pizza>> GetRecipeByIngredients(string first, string second, string third) 
        {
            List<string> ingredients = new List<string> { first, second, third };
            var listOfPizzas = _context.LoadRecords<Pizza>("Pizza");
            var pizzasWithIngredients = new List<Pizza>();
            var list = new List<string>();
            foreach (var pizza in listOfPizzas)
            {
                foreach (var pizzaIngredient in pizza.MustHaveIngredients)
                {
                    if (ingredients.Contains(pizzaIngredient))
                    {
                        list.Add(pizzaIngredient);
                    }
                }
                if(pizza.MustHaveIngredients.SequenceEqual(list)) 
                                                     
                {
                    pizzasWithIngredients.Add(pizza);
                }
            }

            return Ok(pizzasWithIngredients);

            //przeszukiwanie listy pizza przez liste ingredients tak aby pasujace do siebie elementy byly
            //sprawdzane i zostala zwrocona lista obiektow pizza ktora zawiera pasujace skladniki

        }

    }
}
