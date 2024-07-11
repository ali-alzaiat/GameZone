using GameZone.Data;
using GameZone.Models;
using GameZone.Settings;
using GameZone.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class GamesService(ApplicationDBContext context, IWebHostEnvironment webHostEnvironment) : IGamesService
    {
        private readonly ApplicationDBContext _context = context;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
        private readonly string _imagePath = $"{webHostEnvironment.WebRootPath}{FileSettings.ImagePath}";
        public async Task Create(CreateGameFormViewModel model)
        {
            string coverName = await SaveCover(model.Cover);
            //Save the game in database
            Game game = new()
            {
                Name = model.Name,
                Description = model.Description,
                Cover = coverName,
                CategoryId = model.CategoryId,
                Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceID = d }).ToList(),
            };
            _context.Add(game);
            _context.SaveChanges();
        }

        public async Task<Game?> Update(EditGameFormViewModel viewModel)
        {
            var game = _context.Games
                .Include(g => g.Devices)
                .SingleOrDefault(g => g.Id == viewModel.Id);
            if (game is null)
            {
                return null;
            }
            var oldCover = game.Cover;

            game.Name = viewModel.Name;
            game.Description = viewModel.Description;
            game.CategoryId = viewModel.CategoryId;
            game.Devices = viewModel.SelectedDevices.Select(d => new GameDevice { DeviceID=d }).ToList();

            var coverChanged = viewModel.Cover is not null;
            if(coverChanged)
            {
                game.Cover = await SaveCover(viewModel.Cover!);
            }

            var effectedRows = _context.SaveChanges();
            if(effectedRows > 0)
            {
                if (coverChanged)
                {
                    var coverPath = Path.Combine(_imagePath, oldCover);
                    File.Delete(coverPath);
                }
                return game;
            }
            else
            {
                var cover = Path.Combine(_imagePath, game.Cover);
                File.Delete(cover);
                return null;
            }
        }

        public IEnumerable<Game> GetAll()
        {
            var games = _context.Games
                .AsNoTracking()
                .Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device)
                .ToList();
            return games;
        }
        public Game? GetById(int id)
        {
            return _context.Games
                .AsNoTracking()
                .Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device)
                .SingleOrDefault(g => g.Id == id);
        }

        private async Task<string> SaveCover(IFormFile cover)
        {
            //Create a path for the image
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var path = Path.Combine(_imagePath, coverName);
            //Save the image on the server
            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);
            return coverName;
        }

        public bool Delete(int id)
        {
            var game = _context.Games.Find(id);
            if(game is null)
            {
                return false;
            }
            _context.Games.Remove(game);
            var effectedRows = _context.SaveChanges();
            if(effectedRows > 0)
            {
                var path = Path.Combine(_imagePath, game.Cover);
                File.Delete(path);
                return true;
            }
            return false;
        }
    }
}
