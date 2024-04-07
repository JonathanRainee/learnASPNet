﻿using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace JRamedia.Models
{
    public class Books
    {

        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int Price { get; set; }
        public string? Image {  get; set; }

        public int BoughtTimes { get; set; }
    }
}
