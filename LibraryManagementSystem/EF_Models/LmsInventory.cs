using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.EF_Models
{
    public partial class LmsInventory
    {
        public LmsInventory()
        {
            LmsInventoryHistories = new HashSet<LmsInventoryHistory>();
        }

        public int Id { get; set; }
        public string BookTitle { get; set; } = null!;
        public string BookAuthor { get; set; } = null!;
        public string BookVersion { get; set; } = null!;
        public string BookCode { get; set; } = null!;
        public string BookGenre { get; set; } = null!;
        public bool IsIssued { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ImagePath { get; set; }
        public string? ImageBase64 { get; set; }

        public virtual ICollection<LmsInventoryHistory> LmsInventoryHistories { get; set; }
    }
}
