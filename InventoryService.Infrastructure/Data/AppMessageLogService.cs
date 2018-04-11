using InventoryService.Infrastructure.Helper;
using InventoryService.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryService.Infrastructure.Data
{
    /// <summary>
    /// AppmessageLogService object is used to abstract the way of saving the request and response values into DB
    /// </summary>
    /// <remarks>This AppMessageLogService class will invoke HttpWeblogger to save HttpPipeline data into DB</remarks>
    public class AppMessageLogService : IAppMessageLogService, IDisposable
    {
        public void LogData(AppLogEntry log)
        {
            // TODO: Save the request log entry into database
            using (HttpWebLogger logger = new HttpWebLogger())
            {
                logger.LogWebRequest(log);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Logger() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
