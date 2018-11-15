using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {

        static private List<Cheese> Cheeses = new List<Cheese>();
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.cheeses = Cheeses;

            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Remove()
        {
            ViewBag.cheeses = Cheeses;
            return View();
        }

        [HttpPost]
        [Route("/Cheese/Add")]
        public IActionResult NewCheese(string name, string description)
        {
            // Validate entries
            if (name == null || name.Any(char.IsDigit))
            {
                ViewBag.error = "Invalid cheese name";
                return View("Add");
            }


            // Add the new cheese to the existing cheeses
            Cheese addCheese = new Cheese();
            addCheese.Name = name;
            addCheese.Description = description;
            Cheeses.Add(addCheese);
            return Redirect("/Cheese");
        }

        [HttpPost]
        [Route("/Cheese/Remove")]
        public IActionResult RemCheese(string[] name)
        {
            List<string> cheeseNames = new List<string>();
            int idxCheese;
            
            
            // Remove the selected cheeses from the list
            foreach(string select in name)
            {
                foreach (Cheese cheese in Cheeses)
                {
                    cheeseNames.Add(cheese.Name);
                }
                idxCheese = cheeseNames.IndexOf(select);
                Cheeses.Remove(Cheeses[idxCheese]);
                cheeseNames.Clear();
            }
            return Redirect("/Cheese");
        }
        

    }
}
