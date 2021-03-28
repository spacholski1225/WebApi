using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FridgeBook.Models
{
    public abstract class Recipe
    {
        public Guid Id { get; set; }
        public List<string> MustHaveIngredients { get; set; }
        public List<string> OptionalIngredients { get; set; }
    }
}
