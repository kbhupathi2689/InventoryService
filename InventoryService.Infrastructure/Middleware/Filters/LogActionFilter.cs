using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryService.Infrastructure.Middleware.Filters
{
    /// <summary>
    /// MVC Pipeline component for executing actions on controller level
    /// </summary>
    /// <remarks>This Action filter will be used for retrieving HttpRouteTemplate and HttpRouteData</remarks>
    public class LogActionFilter : IActionFilter
    {
        /// <summary>
        /// LogActionFilter Constructor
        /// </summary>
        public LogActionFilter()
        {
        }

        /// <summary>
        /// OnActionExecuted Filter event
        /// </summary>
        /// <remarks>This event will handle ActionExecutedContext</remarks>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        /// <summary>
        /// OnActionExecuting Filter event
        /// </summary>
        /// <remarks>This event will handle ActionExecutingContext</remarks>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionDescriptor.AttributeRouteInfo != null)
                context.HttpContext.Items.Add("RouteTemplate", context.ActionDescriptor.AttributeRouteInfo.Template);
        }
    }
}
