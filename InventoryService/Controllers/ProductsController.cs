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
    /// Products controller
    /// </summary>
    /// <remarks>This controller is used to represent list of products available..</remarks>
    [ServiceFilter(typeof(LogFilter))]
    [Produces("application/json")]
    [Route("api/Products")]
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private ILogger<ProductsController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// ProductsController constructor
        /// </summary>
        /// <remarks>This constructor will use ILogger to capture all event actions.</remarks>
        /// <param>logger</param>
        public ProductsController(ILogger<ProductsController> logger, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        // GET: api/<controller>
        /// <summary>
        /// Http Get Method
        /// </summary>
        /// <remarks>This HttpGet method will return all the list of products.</remarks>
        /// <param></param>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IPaginate<ProductsView> result = null;
            try
            {
                result = await _unitOfWork.GetRepositoryAsync<ProductsView>().GetListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok(result);
        }

        // GET: api/Products/{name}
        /// <summary>
        /// Http Get Method
        /// </summary>
        /// <remarks>This API will return the inventory for a single product.</remarks>
        /// <param name="name">Enter the name</param>
        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            IPaginate<ProductsView> result = null;
            try
            {
                result = await _unitOfWork.GetRepositoryAsync<ProductsView>().GetListAsync(p => p.Name.ToLowerInvariant().Contains(name.ToLowerInvariant()));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok(result);
        }

        // POST api/<controller>
        /// <summary>
        /// Http Post Method
        /// </summary>
        /// <remarks>This API will create a new Product.</remarks>
        /// <param name="value">Enter the Value Object in json</param>
        /*[HttpPost]
        public void Post([FromBody]string value)
        {
            _logger.LogCritical("Hit Post method");
        }*/

        // PUT api/<controller>/5
        /// <summary>
        /// Http Put Method
        /// </summary>
        /// <remarks>This API will update an existing product based on id.</remarks>
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
        /// <remarks>This API will delete an existing product based on Id.</remarks>
        /// <param name="id">Enter the Value Id</param>
        /*[HttpDelete("{id}")]
        public void Delete(int id)
        {
            _logger.LogCritical("Hit Delete method");
        }*/
    }
}
