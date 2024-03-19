using System.ComponentModel.DataAnnotations;

namespace JRamedia.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int Price { get; set; }
    }
}
