namespace LibraryManagementSystem.Models
{
    public class InventorySearchModel
    {
        public string? BookTitle { get; set; } = null;

        public string? BookCode { get; set; } = null;

        public bool? IsIssued { get; set; } = null;

        public DateTime? FromDate { get; set; } = null;

        public DateTime? ToDate { get; set; } = null;
    }
}
