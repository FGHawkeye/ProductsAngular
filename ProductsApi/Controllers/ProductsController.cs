using Application.Services;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ProductsApi.Controllers
{
    [Route("Products")]
    public class ProductsController : ControllerBase
    {
        private ProductService service;

        public ProductsController(ProductService service)
        {
            this.service = service;
        }

        [HttpGet("GetProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<ProductDto>> GetProducts()
        {
            var product = service.GetProducts();
            return product;
        }

        [HttpGet("GetCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Category>> GetCategories()
        {
            var categories = service.GetCategories();
            return categories;
        }

        [HttpPost("AddCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult AddCategory([FromBody] Category category)
        {
            service.AddCategory(category);
            return Ok();
        }

        [HttpPost("AddProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult AddProduct([FromBody] ProductDto product)
        {
            service.AddProduct(product);
            return Ok();
        }

        [HttpPost("UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult UpdateProduct([FromBody] ProductDto product)
        {
            service.UpdateProduct(product);
            return Ok();
        }

        [HttpPost("DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult DeleteProduct([FromBody] ProductDto product)
        {
            service.DeleteProduct(product);
            return Ok();
        }
    }
}
