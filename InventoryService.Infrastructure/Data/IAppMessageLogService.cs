using InventoryService.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryService.Infrastructure.Data
{
    /// <summary>
    /// IAppMessageLogService object is used for abstracting the AppMessageLogService functions
    /// </summary>
    /// <remarks>This IAppMessageLogService Interface object will invoke LogData functionality from middleware pipeline events</remarks>
    public interface IAppMessageLogService
    {
        void LogData(AppLogEntry log);
    }
}
