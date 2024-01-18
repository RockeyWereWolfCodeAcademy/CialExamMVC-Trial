using CialExamMVC_Trial.Areas.Admin.ViewModels.AdminExpertVMs;
using CialExamMVC_Trial.Contexts;
using CialExamMVC_Trial.Helpers;
using CialExamMVC_Trial.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CialExamMVC_Trial.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminExpertController : Controller
    {
        readonly CialDbContext _context;

        public AdminExpertController(CialDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult>  Index()
        {
            var data = await _context.Experts.Select(ex => new AdminExpertListVM
            {
                Id = ex.Id,
                ImgUrl = ex.ImgUrl,
                Content = ex.Content,
            }).ToListAsync();
            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Socials = await _context.Socials.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminExpertCreateVM vm)
        {
            if (vm.Image != null)
            {
                if(!vm.Image.CheckType("image"))
                    ModelState.AddModelError("Image", "File must be an image");
                if (!vm.Image.IsValidSize(1000))
                    ModelState.AddModelError("Image", "Image cannot be larger than 1 mb");
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var dataToCreate = new Expert
            {
                Content = vm.Content,
                ImgUrl = vm.Image.SaveFileAsync(PathConstants.ExpertsImagePath).Result,
                ExpertSocials = vm.SocialIds.Any() ? vm.SocialIds.Select(sId => new ExpertSocial
                {
                    SocialId = sId
                }).ToList() : new List<ExpertSocial>()
            };
            await _context.Experts.AddAsync(dataToCreate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _context.Experts.FindAsync(id);
            if (data == null) return NotFound();
            System.IO.File.Delete(Path.Combine(PathConstants.RootPath, data.ImgUrl));
            _context.Experts.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult>  Update(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _context.Experts.FindAsync(id);
            if (data == null) return NotFound();
            return View(new AdminExpertUpdateVM
            {
                Content = data.Content,
                ImgUrl = data.ImgUrl,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, AdminExpertUpdateVM vm)
        {
            if (id == null) return BadRequest();
            var data = await _context.Experts.FindAsync(id);
            if (data == null) return NotFound();
            if (vm.Image != null)
            {
                if (!vm.Image.CheckType("image"))
                    ModelState.AddModelError("Image", "Image cannot be larger than 1 mb");
                if (!vm.Image.IsValidSize(1000))
                    ModelState.AddModelError("Image", "Image cannot be larger than 1 mb");
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            data.Content = vm.Content;
            if (vm.Image != null)
            {
                System.IO.File.Delete(Path.Combine(PathConstants.RootPath, data.ImgUrl));
                data.ImgUrl = await vm.Image.SaveFileAsync(PathConstants.ExpertsImagePath);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
