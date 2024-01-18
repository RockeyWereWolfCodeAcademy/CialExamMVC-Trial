//using CialExamMVC_Trial.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CialExamMVC_Trial.Controllers
{
    public class HomeController : Controller
    { 
        public IActionResult Index()
        {
            return View();
        }
    }
}