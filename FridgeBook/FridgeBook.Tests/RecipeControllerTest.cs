using FridgeBook.Controllers;
using FridgeBook.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FridgeBook.Tests
{
    public class RecipeControllerTest
    {
        public Recipe Recipe { get; set; }
        public RecipesController Controller{ get; set; }
        public void Setup()
        {
            Recipe = new Recipe
            {
                MustHaveIngredients = new List<string> { "first", "second", "etcetera" }
            };
        }

        [Test]
        public void DeleteById_TestDeletingObject_ExpectTrue()
        {
            Controller.DeleteById(Recipe.Id, "");//nie dziala dopoki w RecipeController nie bedzie _context w DI
        }
    }
}
