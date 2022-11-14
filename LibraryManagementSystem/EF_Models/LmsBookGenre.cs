using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.EF_Models
{
    public partial class LmsBookGenre
    {
        public LmsBookGenre()
        {
            LmsInventories = new HashSet<LmsInventory>();
        }

        public int BookGenreId { get; set; }
        public string BookGenre { get; set; } = null!;

        public virtual ICollection<LmsInventory> LmsInventories { get; set; }
    }
}
