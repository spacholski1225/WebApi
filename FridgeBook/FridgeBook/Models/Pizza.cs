using System;
using System.Collections.Generic;

namespace FridgeBook.Models
{
    public class Pizza
    {
        public Guid Id { get; set; }
        public List<string> MustHaveIngredients { get; set; }
        public List<string> OptionalIngredients { get; set; }

        
    }
}