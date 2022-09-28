using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter Email to proceed ")]
        public String? Email { get; set; } = null;
        [Required(ErrorMessage = "Please enter Password to proceed ")]
        public String? Password { get; set; } = null;
    }
}
