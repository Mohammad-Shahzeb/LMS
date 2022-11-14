using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.EF_Models
{
    public partial class LmsFineCalculation
    {
        public int DueFineId { get; set; }
        public int DueFine { get; set; }
        public bool IsPaid { get; set; }
        public int InventoryRequestHistoryId { get; set; }

        public virtual LmsInventoryHistory InventoryRequestHistory { get; set; } = null!;
    }
}
