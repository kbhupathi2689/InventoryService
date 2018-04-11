using System;
using System.Collections.Generic;

namespace InventoryService.Data.Models
{
    public partial class ProductType
    {
        public ProductType()
        {
            Product = new HashSet<Product>();
            ProductCategory = new HashSet<ProductCategory>();
        }

        public long ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductTypeDesc { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string LastUpdatedUser { get; set; }
        public Guid RowGuid { get; set; }

        public ICollection<Product> Product { get; set; }
        public ICollection<ProductCategory> ProductCategory { get; set; }
    }
}
