using System.ComponentModel.DataAnnotations;

namespace JRamedia.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Userame { get; set; }
        [Required(ErrorMessage = "The Email field is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Password field is required.")]
        public string Password { get; set; }

    }
}
