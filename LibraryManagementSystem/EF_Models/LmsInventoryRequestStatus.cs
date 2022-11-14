using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.EF_Models
{
    public partial class LmsInventoryRequestStatus
    {
        public LmsInventoryRequestStatus()
        {
            LmsInventoryRequests = new HashSet<LmsInventoryRequest>();
        }

        public int RequestStatusId { get; set; }
        public string? RequestStatusDescription { get; set; }

        public virtual ICollection<LmsInventoryRequest> LmsInventoryRequests { get; set; }
    }
}
