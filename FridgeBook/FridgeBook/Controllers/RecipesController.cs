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
        public ActionResult<Pizza> AddRecipeIntoDatabase() //jaki tu powinine byc zwracany typ
        {
            Pizza record = new Pizza
            {
                MustHaveIngredients = new List<string> { "Flour", "Yeast", "Cheese" }
            };

             _context.InsertRecord<Pizza>("Pizza", record);
            return Ok();
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Pizza>> GetRecipeByIngredients(List<string> ingredients) //trzeba dorobic metode w MongoDbCRUD ktora znajduje rekordy z zawieranymi skladnikami
        {
            var listOfPizzas = _context.LoadRecords<Pizza>("Pizza");
            var pizzasWithIngredients = new List<Pizza>();
            var newPizza = new Pizza();
            foreach (var pizza in listOfPizzas)
            {
                foreach (var pizzaIngredient in pizza.MustHaveIngredients)
                {
                    if (ingredients.Contains(pizzaIngredient))
                    {
                        newPizza.MustHaveIngredients.Add(pizzaIngredient);
                    }
                }
                if(pizza == newPizza) // porownanie dwoch obiektow czy ich wartosci tj lista MustHaveIngredients jest taka sama
                                        //jesli tak to dodac obiekt pizza do listy pizz zawierajacych te skladniki
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
