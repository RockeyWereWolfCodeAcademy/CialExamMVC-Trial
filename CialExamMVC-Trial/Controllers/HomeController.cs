//using CialExamMVC_Trial.Models;
using CialExamMVC_Trial.Contexts;
using CialExamMVC_Trial.ViewModels.ExpertVMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CialExamMVC_Trial.Controllers
{
    public class HomeController : Controller
    { 
        readonly CialDbContext _context;

        public HomeController(CialDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult>  Index()
        {
            var data = await _context.Experts.Select(ex => new ExpertListVM
            {
                Content = ex.Content,
                ImgUrl = ex.ImgUrl,
            }).ToListAsync();
            return View(data);
        }
    }
}