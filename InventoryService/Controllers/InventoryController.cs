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

namespace InventoryService.Controllers
{
    /// <summary>
    /// Inventory controller
    /// </summary>
    /// <remarks>This controller is used to represent Inventory list of products available..</remarks>
    [ServiceFilter(typeof(LogFilter))]
    [Produces("application/json")]
    [Route("api/Inventory")]
    public class InventoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private ILogger<InventoryController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// InventoryController constructor
        /// </summary>
        /// <remarks>This constructor will use ILogger to capture all event actions.</remarks>
        /// <param>logger</param>
        public InventoryController(ILogger<InventoryController> logger, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        // GET: api/<controller>
        /// <summary>
        /// Http Get Method
        /// </summary>
        /// <remarks>This HttpGet method will return all the Inventory list of products.</remarks>
        /// <param></param>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IPaginate<InventoryView> result = null;
            try
            {
                result = await _unitOfWork.GetRepositoryAsync<InventoryView>().GetListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok(result);
        }

        // GET api/<controller>/{inventoryname}
        /// <summary>
        /// Http Get Method
        /// </summary>
        /// <remarks>This API will return the inventory for a single product.</remarks>
        /// <param name="name">Enter the name</param>
        [HttpGet("{name}")]
        public async Task<IActionResult> GetInventorySingleProduct(string name)
        {
            IPaginate<InventoryView> result = null;
            try
            {
                result = await _unitOfWork.GetRepositoryAsync<InventoryView>().GetListAsync(i => i.InventoryName.ToLowerInvariant().Contains(name.ToLowerInvariant()));
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return Ok(result);
        }

        // POST api/<controller>
        /// <summary>
        /// Http Post Method
        /// </summary>
        /// <remarks>This API will raise a custome DivideByZero Exception in order to log errors and verify it.</remarks>
        /// <param name="id">Enter the id value in json format</param>
        [HttpPost]
        public void Post([FromBody]int id)
        {
            _logger.LogCritical("Hit Post method");
            //To raise exception explicitly to log into database
            int d = 0;
            int r = 0;
            try
            {
                r = id / d;
            }
            catch (DivideByZeroException ex)
            {
                throw ex;
            }
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Http Put Method
        /// </summary>
        /// <remarks>This API will update the existing Inventory based on id.</remarks>
        /// <param name="id">Enter the Value Id</param>
        /// <param name="value">Enter the Value Name</param>
        /*[HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            _logger.LogCritical("Hit Put method");
        }*/

        // DELETE api/<controller>/5
        /// <summary>
        /// Http Delete Method
        /// </summary>
        /// <remarks>This API will delete the existing inventory based on Id.</remarks>
        /// <param name="id">Enter the Value Id</param>
        /*[HttpDelete("{id}")]
        public void Delete(int id)
        {
            _logger.LogCritical("Hit Delete method");
        }*/
    }
}
