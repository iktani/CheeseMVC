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
                CheeseData.Add(addCheeseViewModel.CreateCheese());
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
            Cheese cheeseToEdit = CheeseData.GetById(cheeseId);
            AddEditCheeseViewModel addEditCheeseViewModel = new AddEditCheeseViewModel
            {
                Name = cheeseToEdit.Name,
                Description = cheeseToEdit.Description,
                Rating = cheeseToEdit.Rating,
                Type = cheeseToEdit.Type,
                CheeseId = cheeseId
            };

            return View(addEditCheeseViewModel);
        }

        [HttpPost]
        public IActionResult Edit(AddEditCheeseViewModel addEditCheeseViewModel)
        {
            if (ModelState.IsValid)
            {
                Cheese cheeseToEdit = CheeseData.GetById(addEditCheeseViewModel.CheeseId);
                cheeseToEdit.Name = addEditCheeseViewModel.Name;
                cheeseToEdit.Description = addEditCheeseViewModel.Description;
                cheeseToEdit.Rating = addEditCheeseViewModel.Rating;
                cheeseToEdit.Type = addEditCheeseViewModel.Type;
                return Redirect("/");
            }

            return View(addEditCheeseViewModel);

        }
        

    }
}
