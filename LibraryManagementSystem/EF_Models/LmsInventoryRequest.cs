using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.EF_Models
{
    public partial class LmsInventoryRequest
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int InventoryId { get; set; }
        public DateTime RequestDate { get; set; }
        public int Status { get; set; }
        public int? StaffId { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual LmsInventory Inventory { get; set; } = null!;
        public virtual LmsStaff? Staff { get; set; }
        public virtual LmsStudent Student { get; set; } = null!;
    }
}
