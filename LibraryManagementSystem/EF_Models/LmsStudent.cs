using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.EF_Models
{
    public partial class LmsStudent
    {
        public LmsStudent()
        {
            LmsInventoryHistories = new HashSet<LmsInventoryHistory>();
            LmsInventoryRequests = new HashSet<LmsInventoryRequest>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string RollNo { get; set; } = null!;
        public string Batch { get; set; } = null!;
        public decimal DueFine { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? ImagePath { get; set; }
        public string? ImageBase64 { get; set; }

        public virtual ICollection<LmsInventoryHistory> LmsInventoryHistories { get; set; }
        public virtual ICollection<LmsInventoryRequest> LmsInventoryRequests { get; set; }
    }
}
