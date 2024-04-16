using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Security.Policy;

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
        [Required]
        public int Pages { get; set; }
        [Required]
        public string Publisher { get; set; }
        [Required]
        public DateTime PublishDate { get; set; }
        [Required]
        public float Weight { get; set; }
        [Required]
        public string SerialNumber { get; set; }
        [Required]
        public float Width { get; set; }
        [Required]
        public float Length { get; set; }
        [Required]
        public string Langugae { get; set; }
    }
}
