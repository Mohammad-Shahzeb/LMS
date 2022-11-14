using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.EF_Models
{
    public partial class LmsInventoryHistory
    {
        public LmsInventoryHistory()
        {
            LmsFineCalculations = new HashSet<LmsFineCalculation>();
        }

        public int Id { get; set; }
        public int StudentId { get; set; }
        public int StaffId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? EntryTypeId { get; set; }
        public int InventoryId { get; set; }

        public virtual LmsInventoryEntryType? EntryType { get; set; }
        public virtual LmsInventory Inventory { get; set; } = null!;
        public virtual LmsStaff Staff { get; set; } = null!;
        public virtual LmsStudent Student { get; set; } = null!;
        public virtual ICollection<LmsFineCalculation> LmsFineCalculations { get; set; }
    }
}
