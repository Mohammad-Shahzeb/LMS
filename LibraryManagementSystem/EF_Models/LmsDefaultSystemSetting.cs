using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.EF_Models
{
    public partial class LmsDefaultSystemSetting
    {
        public int Id { get; set; }
        public int IssuedPeriod { get; set; }
        public decimal FinePerDay { get; set; }
    }
}
