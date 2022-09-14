using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class LoginModel
    {
        [Required]
        public String? Email { get; set; } = null;
        [Required]
        public String? Password { get; set; } = null;
    }
}
