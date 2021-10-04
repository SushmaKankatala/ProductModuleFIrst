using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductFirstModule.Database;
using ProductFirstModule.Database.Entities;
using ProductFirstModule.Services;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using log4net.Core;
using Microsoft.Extensions.Logging;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductFirstModule.Controller
{

    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProductMicroServicesController : ControllerBase
    {

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ProductMicroServicesController));
        private readonly IProductMicroservice _microservice;


        public ProductMicroServicesController(IProductMicroservice microservice)
        {
            _log4net.Info("Logger initiated");
            _microservice = microservice;
        }
        


        //public ProductMicroServicesController(IProductMicroservice microservice)
        //{
        //    this.microservice = microservice;
        //}
        /// <summary>
        /// Search Product by Id
        /// </summary>
        /// <return>
        /// Returns Product Details by ID
        /// </return>
        /// <remarks>
        /// Sample request
        /// GET /api/ProductMicroServices
        /// 
        /// </remarks>
        /// <response code="200">Returns Product Details by ID</response>

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Product>> SearchProductById(int id)
        {
            _log4net.Info("Searching product by productId");

            Product product = await _microservice.ProductById(id);
            

            //Product product = await _db.Product_s.FindAsync(id);


            if (product != null)
            {

                _log4net.Info("Product Found By Id and Returned Successfully");
                return product;
            }
            else
            {

                _log4net.Error("Product Not Found By Id");
                return NotFound();
            }
        }

		/// <summary>
		/// List of all Products
		/// </summary>
		/// <return>
		/// Returns List of all products
		/// </return>
		/// <remarks>
		/// Sample request
		/// GET /api/ProductMicroServices
		/// 
		/// </remarks>
		/// <response code="200">Returns List of all products</response>

		[HttpGet("products")]
        public ActionResult<List<Product>> GetProducts()
        {
            List<Product> products =  _microservice.GetProducts();
            return products;
        }

        /// <summary>
        /// Search Product by Name
        /// </summary>
        /// <return>
        /// Returns List of Product Details by Name
        /// </return>
        /// <remarks>
        /// Sample request
        /// GET /api/ProductMicroServices
        /// 
        /// </remarks>
        /// <response code="200">Returns Product Details by Name</response>

        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Product>> SearchProductByName(string name)
        {
            _log4net.Info("Searching product by productName");
            Product product = await _microservice.SearchProductByName(name);
            if (product != null)
            {
                _log4net.Info("Product Found By Name and Returned Successfully");
                return product;
            }
            else
            {
                _log4net.Error("Product Not Found By Name");
                return NotFound();
            }
        }

        /// <summary>
        /// Add Rating to Product
        /// </summary>
        /// <return>
        /// Returns average rating
        /// </return>
        /// <remarks>
        /// Sample request
        /// POST / api/ProductMicroServices
        /// 
        /// </remarks>
        /// <response code="201">Successfully added rating to a product</response>

        [HttpPost("AddProductRating")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddProductRating([FromBody] Rating rating)
        {
            bool result = await _microservice.AddProductRating(rating);
            if (result)
            {
                _log4net.Info("Rating Added Successfully");
                return Ok();
            }
            else
            {
                _log4net.Error("Rating Cannot be Added.Invalid");

                return NotFound();
            }
        }


    }
}
