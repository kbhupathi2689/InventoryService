using System;
using System.Collections.Generic;

namespace InventoryService.Data.Models
{
    public partial class Product
    {
        public Product()
        {
            Inventory = new HashSet<Inventory>();
        }

        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductNumber { get; set; }
        public long ProductTypeId { get; set; }
        public long ProductCategoryId { get; set; }
        public long ProductSubCategoryId { get; set; }
        public bool ProductIsSalable { get; set; }
        public bool ProductIsPurchased { get; set; }
        public decimal StandardCost { get; set; }
        public decimal ProductSellingPrice { get; set; }
        public DateTime? ProductSellStartDate { get; set; }
        public DateTime? ProductSellEndDate { get; set; }
        public DateTime? ProductDiscontinuedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string LastUpdatedUser { get; set; }
        public Guid RowGuid { get; set; }
        public string ProductVersion { get; set; }
        public DateTime ProductReleasedOn { get; set; }

        public ProductCategory ProductCategory { get; set; }
        public ProductSubCategory ProductSubCategory { get; set; }
        public ProductType ProductType { get; set; }
        public ICollection<Inventory> Inventory { get; set; }
    }
}
