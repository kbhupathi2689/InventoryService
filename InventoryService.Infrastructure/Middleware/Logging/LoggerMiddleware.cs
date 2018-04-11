using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Internal;
using InventoryService.Infrastructure.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Routing;
using InventoryService.Infrastructure.Data;

namespace InventoryService.Infrastructure.Middleware.Logging
{
    /// <summary>
    /// Middleware component for logging
    /// </summary>
    /// <remarks>This Logging provider will be used for logging user request and response into DB</remarks>
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private AppLogEntry _appLogEntry;

        /// <summary>
        /// LoggerMiddleware Constructor
        /// </summary>
        /// <remarks>This Constructor will take RequestDelegate</remarks>
        /// <param name="next"></param>
        public LoggerMiddleware(RequestDelegate next)
        {
            _next = next;
            _appLogEntry = new AppLogEntry();
        }

        /// <summary>
        /// Invoke command
        /// </summary>
        /// <remarks>This Invoke command will take HttpContext to read the input request stream and response stream</remarks>
        /// <param name="context"></param>
        public async Task Invoke(HttpContext context)
        {
            using (MemoryStream requestBodyStream = new MemoryStream())
            {
                using (MemoryStream responseBodyStream = new MemoryStream())
                {
                    Stream originalRequestBody = context.Request.Body;
                    context.Request.EnableRewind();
                    Stream originalResponseBody = context.Response.Body;
                    string bearerToken = string.Empty;
                    try
                    {
                        await context.Request.Body.CopyToAsync(requestBodyStream);
                        requestBodyStream.Seek(0, SeekOrigin.Begin);

                        string requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();

                        requestBodyStream.Seek(0, SeekOrigin.Begin);
                        context.Request.Body = requestBodyStream;

                        FormatRequest(ref _appLogEntry, ref context, requestBodyText);

                        //Retrieve Bearer token from HttpRequest Authorization Headers
                        var authHeader = context.Request.Headers["Authorization"].ToString();
                        if (authHeader != null && authHeader.StartsWith("bearer", StringComparison.OrdinalIgnoreCase))
                        {
                            bearerToken = authHeader.Substring("Bearer ".Length).Trim();
                        }

                        string responseBody = "";

                        context.Response.Body = responseBodyStream;

                        //Stopwatch watch = Stopwatch.StartNew();

                        await _next(context);

                        //watch.Stop();

                        responseBodyStream.Seek(0, SeekOrigin.Begin);
                        responseBody = new StreamReader(responseBodyStream).ReadToEnd();

                        if (context.Items.Count > 0)
                        {
                            if (context.Items.ContainsKey("RouteTemplate"))
                            {
                                _appLogEntry.RequestRouteTemplate = context.Items["RouteTemplate"].ToString();
                            }
                        }
                        if (context.GetRouteData() != null)
                        {
                            _appLogEntry.RequestRouteData = JsonConvert.SerializeObject(context.GetRouteData().Values, Newtonsoft.Json.Formatting.Indented);
                        }
                        _appLogEntry.ResponseTimestamp = DateTime.Now;
                        _appLogEntry.ResponseStatusCode = context.Response.StatusCode;
                        _appLogEntry.ResponseContentType = context.Response.ContentType;
                        _appLogEntry.ResponseContentBody = responseBody;
                        _appLogEntry.ResponseHeaders = JsonConvert.SerializeObject(context.Response.Headers.ToList<KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues>>(), Newtonsoft.Json.Formatting.Indented);

                        if (_appLogEntry != null)
                        {
                           using (AppMessageLogService logger = new AppMessageLogService())
                            {
                                try
                                {
                                    if (!String.IsNullOrWhiteSpace(_appLogEntry.User))
                                    {
                                        logger.LogData(_appLogEntry);
                                    }
                                    else if (_appLogEntry.ResponseStatusCode == 401)
                                    {
                                        //User is unauthorized hence use the machine name as user to log this particular case..
                                        if (String.IsNullOrWhiteSpace(_appLogEntry.User))
                                        {
                                            _appLogEntry.User = _appLogEntry.Machine;
                                            logger.LogData(_appLogEntry);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    string msg = ex.Message;
                                }
                            }
                        }

                        responseBodyStream.Seek(0, SeekOrigin.Begin);

                        await responseBodyStream.CopyToAsync(originalResponseBody);
                    }
                    catch (Exception ex)
                    {
                        byte[] data = System.Text.Encoding.UTF8.GetBytes(ex.Message.ToString());
                        originalResponseBody.Write(data, 0, data.Length);

                        if (context.Items.Count > 0)
                        {
                            if (context.Items.ContainsKey("RouteTemplate"))
                            {
                                _appLogEntry.RequestRouteTemplate = context.Items["RouteTemplate"].ToString();
                            }
                        }
                        if (context.GetRouteData() != null)
                        {
                            _appLogEntry.RequestRouteData = JsonConvert.SerializeObject(context.GetRouteData().Values, Newtonsoft.Json.Formatting.Indented);
                        }
                        _appLogEntry.ResponseTimestamp = DateTime.Now;
                        _appLogEntry.ResponseStatusCode = 500;
                        _appLogEntry.ResponseContentType = "text/plain";
                        _appLogEntry.ResponseContentBody = ex.Message.ToString();
                        _appLogEntry.ResponseHeaders = JsonConvert.SerializeObject(context.Response.Headers.ToList<KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues>>(), Newtonsoft.Json.Formatting.Indented);

                        //send exception details via Serilog email...
                        Serilog.Log.Error(ex, "Exception occurred while performing an action by User: {@userid} having an Authorization bearer token value: {@token} in the HttpRequest headers. If you want to decode the token kindly visit https://jwt.io/ and paste the token value in the Encoded section area. Here are the exception details: ", _appLogEntry.User, bearerToken);

                        if (_appLogEntry != null)
                        {
                            using (AppMessageLogService logger = new AppMessageLogService())
                            {
                                try
                                {
                                    if (!String.IsNullOrWhiteSpace(_appLogEntry.User))
                                    {
                                        logger.LogData(_appLogEntry);
                                    }
                                    else if (_appLogEntry.ResponseStatusCode == 401)
                                    {
                                        //User is unauthorized hence use the machine name as user to log this particular case..
                                        if (String.IsNullOrWhiteSpace(_appLogEntry.User))
                                        {
                                            _appLogEntry.User = _appLogEntry.Machine;
                                            logger.LogData(_appLogEntry);
                                        }
                                    }
                                }
                                catch (Exception exLog)
                                {
                                    string msg = exLog.Message;
                                }
                            }
                        }
                    }
                    finally
                    {
                        context.Request.Body = originalRequestBody;
                        context.Response.Body = originalResponseBody;
                    }
                }
            }
        }

        /// <summary>
        /// FormatRequest method
        /// </summary>
        /// <remarks>This FormatRequest method will take ref of AppLogEntry object, ref of HttpContext object </remarks>
        /// <param name="appLogEntry"></param>
        /// <param name="context"></param>
        /// <param name="requestBodyText"></param>
        private void FormatRequest(ref AppLogEntry appLogEntry, ref HttpContext context, string requestBodyText)
        {
            _appLogEntry.AppMessageEntryId = Guid.NewGuid().ToString();
            _appLogEntry.ApplicationName = "Inventory";
            _appLogEntry.User = GetUserName(context.User.Claims);
            _appLogEntry.Machine = Environment.MachineName;
            _appLogEntry.RequestContentType = context.Request.ContentType;
            _appLogEntry.RequestContentBody = requestBodyText;
            _appLogEntry.RequestIpAddress = context.Connection.LocalIpAddress.ToString();
            _appLogEntry.RequestMethod = context.Request.Method;
            _appLogEntry.RequestHeaders = JsonConvert.SerializeObject(context.Request.Headers.ToList<KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues>>(), Newtonsoft.Json.Formatting.Indented);
            _appLogEntry.RequestTimestamp = DateTime.Now;
            _appLogEntry.RequestUri = context.Request.GetDisplayUrl();
        }

        /// <summary>
        /// GetUserName method
        /// </summary>
        /// <remarks>This GetUserName method will retrieve user email address from the list of claims </remarks>
        /// <param name="clmList"></param>
        private string GetUserName(IEnumerable<System.Security.Claims.Claim> clmList)
        {
            string userName = String.Empty;

            var user = from c in clmList
                       where c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
                       select c.Value;

            if (user != null)
            {
                userName = user.FirstOrDefault();
                if(String.IsNullOrWhiteSpace(userName))
                {
                    userName = Environment.MachineName; //if user name return is null then set the request machine name
                }
            }
            else
            {
                userName = Environment.MachineName; //if no authentication is being set..
            }
            return userName;
        }
    }
}
