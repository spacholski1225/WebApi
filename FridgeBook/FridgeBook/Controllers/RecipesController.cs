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
        public ActionResult<Recipe> AddRecipeIntoDatabase() 
        {
            Recipe record = new Recipe
            {
                MustHaveIngredients = new List<string> { "Flour", "Yeast", "Cheese" }
            };

             _context.InsertRecord<Recipe>("Recipes", record);
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
        public ActionResult<IEnumerable<Recipe>> GetRecipeByIngredients(string first, string second, string third) //dodac model podawanych instrukcji
        {
            List<string> ingredients = new List<string> { first, second, third };
            var listOfRecipes = _context.LoadRecords<Recipe>("Pizza");
            var dish = new List<Recipe>();
            var list = new List<string>();

            foreach (var recipe in listOfRecipes)
            {
                foreach (var recIng in recipe.MustHaveIngredients)
                {
                    if (ingredients.Contains(recIng))
                        list.Add(recIng);
                }

                if (recipe.MustHaveIngredients.SequenceEqual(list))
                    dish.Add(recipe);
            }
            return Ok(dish);
        }

        [HttpDelete]
        public ActionResult DeleteById(Guid id, string table)
        {
            _context.DeleteRecord<Recipe>(table, id);

            return Ok();
            
        }


    }
}
