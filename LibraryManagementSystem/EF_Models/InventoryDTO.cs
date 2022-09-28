
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.EF_Models
{
    [ModelMetadataType(typeof(InventoryDTO))]
    public partial class LmsInventory

    { 

     
    }

    public class InventoryDTO
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage ="Book Title Required")]
        public string BookTitle { get; set; } = null!;

        [Required(ErrorMessage = "Book Title Required")]
        public string BookAuthor { get; set; } = null!;

        [Required(ErrorMessage = "Book Title Required")]
        public string BookVersion { get; set; } = null!;

        [Required(ErrorMessage = "Book Title Required")]
        public string BookCode { get; set; } = null!;

        [Required(ErrorMessage = "Book Title Required")]
        public string BookGenre { get; set; } = null!;

        [Required(ErrorMessage = "Book Title Required")]
        public bool IsIssued { get; set; }
        public string? ImagePath { get; set; }
    }
}
