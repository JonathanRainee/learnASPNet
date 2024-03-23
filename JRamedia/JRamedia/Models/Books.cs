using MessagePack;
using System.ComponentModel.DataAnnotations;

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
    }
}
