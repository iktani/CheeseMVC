using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.Models
{
    public class Cheese
    {
        private static int nextCheeseId = 1;
        public int CheeseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public CheeseType Type { get; set; }

        public Cheese() 
        {
            CheeseId = nextCheeseId;
            nextCheeseId++;
        }

        
        
            


    }

}
