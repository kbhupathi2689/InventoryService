using System;
using System.Collections.Generic;

namespace InventoryService.Data.Models
{
    public partial class Manufacturer
    {
        public Manufacturer()
        {
            Inventory = new HashSet<Inventory>();
        }

        public long ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public string ManufacturerAddress { get; set; }
        public string ManufacturerContact { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string LastUpdatedUser { get; set; }
        public Guid RowGuid { get; set; }

        public ICollection<Inventory> Inventory { get; set; }
    }
}
