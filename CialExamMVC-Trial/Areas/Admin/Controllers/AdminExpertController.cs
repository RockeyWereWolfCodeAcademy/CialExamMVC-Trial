using Microsoft.AspNetCore.Mvc;

namespace CialExamMVC_Trial.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminExpertController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
