using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.EF_Models
{
    public partial class LmsInventoryEntryType
    {
        public LmsInventoryEntryType()
        {
            LmsInventoryHistories = new HashSet<LmsInventoryHistory>();
        }

        public int EntryTypeId { get; set; }
        public string EntryType { get; set; } = null!;

        public virtual ICollection<LmsInventoryHistory> LmsInventoryHistories { get; set; }
    }
}
