
//using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.EF_Models
{
    [ModelMetadataType(typeof(StudentDTO))]
    public partial class LmsStudent
    {

    }



    public class StudentDTO
    {
   
        public int Id { get; set; }

        [Required (ErrorMessage ="First Name is Required")]
        public string FirstName { get; set; } = null!;

        [Required (ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; } = null!;

        [Required (ErrorMessage = "Roll No is Required")]
        public string RollNo { get; set; } = null!;

        [Required (ErrorMessage = "Batch is Required")]
        public string Batch { get; set; } = null!;

        [Required (ErrorMessage = "Email is Required")]
        public string Email { get; set; } = null!;

        [Required (ErrorMessage = "Password is Required")]
        public string Password { get; set; } = null!;
    }
}
