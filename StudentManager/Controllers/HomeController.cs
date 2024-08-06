using Microsoft.AspNetCore.Mvc;
using StudentManager.Models;
using System.Diagnostics;

namespace StudentManager.Controllers
{
    public class HomeController : Controller
    {
        

        
        public IActionResult Index()
        {

            return View();
        }

       
    }
}
