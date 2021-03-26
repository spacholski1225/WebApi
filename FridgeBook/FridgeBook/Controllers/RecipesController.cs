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
        public void AddRecipeIntoDatabase() //jaki tu powinine byc zwracany typ
        {
            Pizza record = new Pizza
            {
                MustHaveIngredients = new List<string> { "Flour", "Yeast", "Cheese" }
            };

             _context.InsertRecord<Pizza>("Pizza", record);
        }
        
        [HttpGet]
        public void GetRecipeByIngredients(List<string> ingredients) //trzeba dorobic metode w MongoDbCRUD ktora znajduje rekordy z zawieranymi skladnikami
        {

            var listOfPizzas = _context.LoadRecords<Pizza>("Pizza");

            //przeszukiwanie listy pizza przez liste ingredients tak aby pasujace do siebie elementy byly
            //sprawdzane i zostala zwrocona lista obiektow pizza ktora zawiera pasujace skladniki

        }

    }
}
