using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Crud.Models;
using Microsoft.EntityFrameworkCore;

namespace Crud.Controllers
{
    public class HomeController : Controller
    {
        private CrudContext dbContext;
        public HomeController(CrudContext context)
        {
            dbContext = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.AllDishes = dbContext.Dishes.ToList();
            // ViewBag.Chef = dbContext.Dishes.Where(d => d.Chef == "Lawrence");
            // ViewBag.MostRecent = dbContext.Dishes.OrderByDescending(d => d.CreatedAt)
            // .Take(5)
            // .ToList();
            return View();
        }

        [HttpGet("/new")]
        public IActionResult NewDish(Dish newDish)
        {
            ViewBag.Dish = newDish;
            return View("AddDish");
        }
        [HttpPost("create")]
        public IActionResult CreateDish(Dish newDish)
        {
            dbContext.Add(newDish);
            ViewBag.Dish = newDish;
            dbContext.SaveChanges();
            return View("newDish", newDish);
        }

        [HttpGet("/dish/{dishId}")]
        public IActionResult ViewDish(Dish dish)
        {
            ViewBag.Dish = dbContext.Dishes.FirstOrDefault(d => d.DishID == dish.DishID);
            return View("NewDish", dish);
        }

        [HttpGet("/delete/{dishId}")]
        public IActionResult DeleteDish(int dishId)
        {
            Dish RetrievedDish = dbContext.Dishes.SingleOrDefault(d => d.DishID == dishId);

            dbContext.Dishes.Remove(RetrievedDish);

            dbContext.SaveChanges();
            ViewBag.AllDishes = dbContext.Dishes.ToList();
            return RedirectToAction("");
        }

        [HttpGet("/edit/{dishId}")]
        public IActionResult EditDish(int dishId)
        {
            Dish RetrievedDish = dbContext.Dishes.FirstOrDefault(dish => dish.DishID == dishId);

            return View("EditDish", RetrievedDish);
        }

        [HttpPost("/change/{dishId}")]
        public IActionResult Change(int dishId, Dish updatedDish)
        {
            Dish RetrievedDish = dbContext.Dishes.FirstOrDefault(dish => dish.DishID == dishId);
            RetrievedDish.Name = updatedDish.Name;
            RetrievedDish.Chef = updatedDish.Chef;
            RetrievedDish.Calories = updatedDish.Calories;
            RetrievedDish.Tastiness = updatedDish.Tastiness;
            RetrievedDish.Description = updatedDish.Description;
            dbContext.Dishes.Update(RetrievedDish);
            dbContext.SaveChanges();

            return RedirectToAction("ViewDish", new { dishId = dishId });
        }


        public IActionResult GetOneChef(string Name)
        {
            Dish single = dbContext.Dishes.FirstOrDefault(dish => dish.Name == Name);
            return View();
        }






        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
