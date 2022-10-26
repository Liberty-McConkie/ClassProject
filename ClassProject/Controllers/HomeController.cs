using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassProject.Models;
using ClassProject.Models.ViewModels;

namespace ClassProject.Controllers
{
    public class HomeController : Controller
    {
        private IClassProjectRepository repo;

        public IActionResult Index()
        {
            return View();
        }
    }
}
