using System;
using System.Collections.Generic;

namespace InventoryService.Data.Models
{
    public partial class ProductSubCategory
    {
        public ProductSubCategory()
        {
            Product = new HashSet<Product>();
        }

        public long ProductSubCategoryId { get; set; }
        public string ProductSubCategoryName { get; set; }
        public long ProductCategoryId { get; set; }
        public string SupportedPlatform { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string LastUpdatedUser { get; set; }
        public Guid RowGuid { get; set; }
        public long DomainId { get; set; }

        public Domain Domain { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
