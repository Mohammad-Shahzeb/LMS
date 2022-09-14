namespace LibraryManagementSystem.Models
{
    public class StaffSearchModel
    {
        public string? FirstName { get; set; } = null;

        public string? LastName { get; set; } = null;

        public string? Email { get; set; } = null;

        public DateTime? FromDate { get; set; } = null;

        public DateTime? ToDate { get; set; } = null;
    }
}
