using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.EF_Models
{
    [ModelMetadataType(typeof(BookGenreDTO))]
    public partial class LmsBookGenre
    {

    }

    public class BookGenreDTO
    {
        public int BookGenreId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Genre can be Max of 50 Letters Length")]
        [Remote("IsValidGenre", "Genre", ErrorMessage = ("Genre Must be Unique"),AdditionalFields = "BookGenreId")]
        public string BookGenre { get; set; } = null!;
    }
}
