using GameZone.Data;
using GameZone.Services;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Controllers
{
    public class GamesController(ICategoriesService categoriesService, IDevicesService devicesService, IGamesService gamesService) : Controller
    {
        private readonly ICategoriesService _categoriesService = categoriesService;
        private readonly IDevicesService _devicesService = devicesService;
        private readonly IGamesService _gamesService = gamesService;
        public IActionResult Index()
        {
            var games = _gamesService.GetAll();
            return View(games);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormViewModel viewModel = new()
            {
                Categories = _categoriesService.GetSelectList(),
                Devices = _devicesService.GetSelectList()
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesService.GetSelectList();
                model.Devices = _devicesService.GetSelectList();
                return View(model);
            }
            await _gamesService.Create(model);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var game = _gamesService.GetById(id);
            if(game is null)
            {
                return NotFound();
            }
            return View(game);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game = _gamesService.GetById(id);
            if(game is null)
            {
                return NotFound();
            }
            EditGameFormViewModel viewModel = new()
            {
                Id = id,
                Name = game.Name,
                Description = game.Description,
                CategoryId = game.CategoryId,
                SelectedDevices = game.Devices.Select(d => d.DeviceID).ToList(),
                Categories = _categoriesService.GetSelectList(),
                Devices = _devicesService.GetSelectList(),
                currentCover = game.Cover
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesService.GetSelectList();
                model.Devices = _devicesService.GetSelectList();
                return View(model);
            }
            var game = await _gamesService.Update(model);
            if(game is null)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
