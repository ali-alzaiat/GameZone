using GameZone.Data;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Controllers
{
    public class GamesController(ApplicationDBContext context) : Controller
    {
        private readonly ApplicationDBContext _context = context;
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormViewModel viewModel = new()
            {
                Categories = _context.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name})
                .OrderBy(comparer => comparer.Text)
                .ToList(),
                Devices = _context.Devices.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .OrderBy(comparer => comparer.Text)
                .ToList()
            };
            return View(viewModel);
        }
    }
}
