using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.EF_Models
{
    public partial class LmsInventory
    {
        public LmsInventory()
        {
            LmsInventoryHistories = new HashSet<LmsInventoryHistory>();
            LmsInventoryRequests = new HashSet<LmsInventoryRequest>();
        }

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

        public virtual LmsInventoryCode BookCodeNavigation { get; set; } = null!;
        public virtual ICollection<LmsInventoryHistory> LmsInventoryHistories { get; set; }
        public virtual ICollection<LmsInventoryRequest> LmsInventoryRequests { get; set; }
    }
}
