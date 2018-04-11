using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryService.Data.Models
{
    public partial class InventoryView
    {
        public long ID { get; set; }
        public string InventoryName { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }
        public string Domain { get; set; }
        public string Manufacturer { get; set; }
        public string Product { get; set; }
        public string Shelf { get; set; }
        public string Container { get; set; }
        public string StockAvailable { get; set; }
        public int StockCount { get; set; }
    }
}
