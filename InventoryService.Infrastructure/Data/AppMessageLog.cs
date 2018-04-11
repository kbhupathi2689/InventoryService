using System;
using System.Collections.Generic;

namespace InventoryService.Infrastructure.Data
{
    public partial class AppMessageLog
    {
        public long Id { get; set; }
        public string AppMessageEntryId { get; set; }
        public string ApplicationName { get; set; }
        public string User { get; set; }
        public string Machine { get; set; }
        public string RequestIpAddress { get; set; }
        public string RequestContentType { get; set; }
        public string RequestContentBody { get; set; }
        public string RequestUri { get; set; }
        public string RequestMethod { get; set; }
        public string RequestRouteTemplate { get; set; }
        public string RequestRouteData { get; set; }
        public string RequestHeaders { get; set; }
        public DateTime? RequestTimestamp { get; set; }
        public string ResponseContentType { get; set; }
        public string ResponseContentBody { get; set; }
        public int? ResponseStatusCode { get; set; }
        public string ResponseHeaders { get; set; }
        public DateTime? ResponseTimestamp { get; set; }
    }
}
