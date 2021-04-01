using FridgeBook.Controllers;
using FridgeBook.Models;
using Microsoft.AspNetCore.Mvc;
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
        private Recipe Recipe { get; set; }
        public RecipesController Controller{ get; set; }
        [SetUp]
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
            var result = Controller.DeleteById(Recipe.Id, "Recipes");
            
        }

        [Test]
        public void AddRecipeIntoDatabase_TestAddToDatabase_ExpectTrue()
        {
            var result = Controller.AddRecipeIntoDatabase(Recipe);

            Assert.IsNotNull(result);
            
        }
    }
}
