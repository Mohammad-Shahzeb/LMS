using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.EF_Models
{
    public partial class LmsStaff
    {
        public LmsStaff()
        {
            LmsInventoryHistories = new HashSet<LmsInventoryHistory>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? ImagePath { get; set; }
        public string? ImageBase64 { get; set; }

        public virtual ICollection<LmsInventoryHistory> LmsInventoryHistories { get; set; }
    }
}
