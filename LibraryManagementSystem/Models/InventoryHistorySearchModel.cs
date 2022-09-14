namespace LibraryManagementSystem.Models
{
    public class InventoryHistorySearchModel
    {
        public string? StudentName { get; set; } = null;
        public string? StaffName { get; set; } = null;
        public string? InventoryTitle { get; set; } = null;
        public DateTime? FromDate { get; set; } = null;
        public DateTime? ToDate { get; set; } = null;
        public string? EntryType { get; set; } = null;
    }
}
