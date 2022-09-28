namespace LibraryManagementSystem.Models
{
    public class AvailableInventoryItem
    {
        public int Id { get; set; }
        public string BookTitle { get; set; } = null!;
        public string BookAuthor { get; set; } = null!;
        public string BookVersion { get; set; } = null!;
        public int BookCode { get; set; }
        public string BookGenre { get; set; } = null!;
        public bool IsIssued { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ImagePath { get; set; }
        public string? ImageBase64 { get; set; }
        public bool alreadyRequested { get; set; } = false;
    }
}
