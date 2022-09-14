using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.EF_Models
{
    public partial class LmsInventoryHistory
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int StaffId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string EntryType { get; set; } = null!;
        public int FineCollected { get; set; }
        public int InventoryId { get; set; }

        public virtual LmsInventory Inventory { get; set; } = null!;
        public virtual LmsStaff Staff { get; set; } = null!;
        public virtual LmsStudent Student { get; set; } = null!;
    }
}
