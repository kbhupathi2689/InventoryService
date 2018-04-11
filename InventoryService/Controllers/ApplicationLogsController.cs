using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using InventoryService.Infrastructure.Middleware.Logging.Web.Filters;
using InventoryService.Repository;
using InventoryService.Data.Models;
using InventoryService.Repository.Paging;
using InventoryService.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using InventoryService.Repository.DIExtensions;

namespace InventoryService.Controllers
{
    /// <summary>
    /// ApplicationLogs controller
    /// </summary>
    /// <remarks>This controller is used to retrieve list of Application logs..</remarks>
    [ServiceFilter(typeof(LogFilter))]
    [Produces("application/json")]
    [Route("api/ApplicationLogs")]
    public class ApplicationLogsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private ILogger<ApplicationLogsController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        LogManagerDbContext logDbContext = new LogManagerDbContext();

        /// <summary>
        /// ApplicationLogsController constructor
        /// </summary>
        /// <remarks>This constructor will use ILogger to capture all event actions.</remarks>
        /// <param>logger</param>
        public ApplicationLogsController(ILogger<ApplicationLogsController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = new UnitOfWork<LogManagerDbContext>(logDbContext);
        }

        // GET: api/<controller>
        /// <summary>
        /// Http Get Method
        /// </summary>
        /// <remarks>This HttpGet method will return all the list of application logs.</remarks>
        /// <param></param>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IPaginate<AppMessageLog> result = null;
            try
            {
                result = await _unitOfWork.GetRepositoryAsync<AppMessageLog>().GetListAsync(orderBy: o => o.OrderByDescending(t => t.Id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok(result);
        }
    }
}
