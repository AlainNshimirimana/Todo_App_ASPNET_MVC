using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Todo_App_ASPNET_MVC.Models;

namespace Todo_App_ASPNET_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly TodoDbContext _db;
        public HomeController(TodoDbContext todoDbContext)
        {
            _db = todoDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
