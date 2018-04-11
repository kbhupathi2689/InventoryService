using System;
using System.Collections.Generic;

namespace InventoryService.Data.Models
{
    public partial class Inventory
    {
        public long InventoryId { get; set; }
        public string InventoryName { get; set; }
        public string InventoryAddress { get; set; }
        public string InventoryPrimaryContact { get; set; }
        public string InventorySecondaryContact { get; set; }
        public long ManufacturerId { get; set; }
        public long ProductId { get; set; }
        public string ShelfLocation { get; set; }
        public string Bin { get; set; }
        public bool InStock { get; set; }
        public int QuantityOnHand { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string LastUpdatedUser { get; set; }
        public Guid RowGuid { get; set; }
        public long DomainId { get; set; }

        public Domain Domain { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public Product Product { get; set; }
    }
}
