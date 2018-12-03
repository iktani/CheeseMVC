using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Cheese> cheeses = CheeseData.GetAll();

            return View(cheeses);
        }

        public IActionResult Add()
        {
            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel();
            return View(addCheeseViewModel);
        }

        public IActionResult Remove()
        {
            ViewBag.title = "Remove Cheeses";
            ViewBag.cheeses = CheeseData.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
        {
            if (ModelState.IsValid)
            {
                Cheese newCheese = new Cheese
                {
                    Name = addCheeseViewModel.Name,
                    Description = addCheeseViewModel.Description,
                    Type = addCheeseViewModel.Type
                };

                CheeseData.Add(newCheese);
                return Redirect("/Cheese");

            }

            return View(addCheeseViewModel);
            
        }

        [HttpPost]
        [Route("/Cheese/Remove")]
        public IActionResult Remove(int[] cheeseIds)
        {
            // Remove the selected cheeses from the list
            foreach(int cheeseId in cheeseIds)
            {
                CheeseData.Remove(cheeseId);
            }
            return Redirect("/");
        }

        public IActionResult Edit(int cheeseId)
        {
            ViewBag.cheese = CheeseData.GetById(cheeseId);
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int cheeseId, string name, string description)
        {
            Cheese cheeseToEdit = CheeseData.GetById(cheeseId);
            cheeseToEdit.Name = name;
            cheeseToEdit.Description = description;
            return Redirect("/");

        }
        

    }
}
