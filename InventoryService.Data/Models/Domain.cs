using System;
using System.Collections.Generic;

namespace InventoryService.Data.Models
{
    public partial class Domain
    {
        public Domain()
        {
            Inventory = new HashSet<Inventory>();
            ProductSubCategory = new HashSet<ProductSubCategory>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string LastUpdatedUser { get; set; }
        public Guid RowGuid { get; set; }

        public ICollection<Inventory> Inventory { get; set; }
        public ICollection<ProductSubCategory> ProductSubCategory { get; set; }
    }
}
