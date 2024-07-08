﻿using GameZone.Models;
using GameZone.ViewModels;

namespace GameZone.Services
{
    public interface IGamesService
    {
        Task Create(CreateGameFormViewModel viewModel);
        IEnumerable<Game> GetAll();
    }
}
