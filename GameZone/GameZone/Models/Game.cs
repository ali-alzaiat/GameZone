﻿using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
    public class Game
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;
        [MaxLength(500)]
        public string Cover { get; set; } = string.Empty;
        public int CategorId { get; set; }
        public Category Category { get; set; } = default!;

        public ICollection<GameDevice> Device { get; set;} = new List<GameDevice>();

    }
}
