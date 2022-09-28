using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.EF_Models
{
    public partial class LmsInventoryCode
    {
        public LmsInventoryCode()
        {
            LmsInventories = new HashSet<LmsInventory>();
        }

        public int Id { get; set; }
        public string? Code { get; set; }

        public virtual ICollection<LmsInventory> LmsInventories { get; set; }
    }
}
