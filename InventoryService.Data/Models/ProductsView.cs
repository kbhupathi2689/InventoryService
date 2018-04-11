using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryService.Data.Models
{
    public partial class ProductsView
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Version { get; set; }
        public string Platform { get; set; }
        public decimal StandardCost { get; set; }
        public decimal SellingPrice { get; set; }
        public DateTime ReleasedDate { get; set; }
    }
}
