using System;
using System.Collections.Generic;

namespace InventoryService.Data.Models
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            Product = new HashSet<Product>();
            ProductSubCategory = new HashSet<ProductSubCategory>();
        }

        public long ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public string ProductCategoryDesc { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string LastUpdatedUser { get; set; }
        public Guid RowGuid { get; set; }
        public long ProductTypeId { get; set; }

        public ProductType ProductType { get; set; }
        public ICollection<Product> Product { get; set; }
        public ICollection<ProductSubCategory> ProductSubCategory { get; set; }
    }
}
