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
