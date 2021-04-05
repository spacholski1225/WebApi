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
            _context = new MongoDbCRUD("Recipes");
        }

        [HttpPost]
        public ActionResult<Recipe> AddRecipeIntoDatabase(Recipe recipe)
        {
            _context.InsertRecord<Recipe>("Recipes", recipe);
            return Ok(recipe);
        }

        /// <summary>
        /// Function retrives the recipe according to the users requirments 
        /// </summary>
        /// <param name="first">First ingredient</param>
        /// <param name="second">Second ingredient</param>
        /// <param name="third">Third ingredient</param>
        /// <returns>Returns an objects that meets the requirments</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Recipe>> GetRecipeByIngredients(string first, string second, string third) //dodac model ktory zawiera liste podawanych skladnikow
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

        [HttpPut]
        public Recipe UpdateRecipeById(Recipe recipe, string table)
        {
            return _context.UpsertRecord<Recipe>(table, recipe, recipe.Id);
        }

        [HttpDelete]
        public ActionResult DeleteById(Guid id, string table)
        {
            var recipe = _context.LoadRecordById<Recipe>("Recipes", id);
            if (recipe != null)
            {
                _context.DeleteRecord<Recipe>(table, id);
                return Ok();
            }
            return NotFound();


        }


    }
}
