﻿using GameZone.Data;
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
            //Create a path for the image
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}";
            var path = Path.Combine(_imagePath,coverName);
            //Save the image on the server
            using var stream = File.Create(path);
            await model.Cover.CopyToAsync(stream);
            //Save the game in database
            Console.WriteLine(model.CategoryId);
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
    }
}
