using System;
using System.Collections.Generic;

namespace InventoryService.Infrastructure.Data
{
    public partial class AppLogs
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Thread { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public string Context { get; set; }
    }
}
