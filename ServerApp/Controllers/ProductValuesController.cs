using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch;
using System.Text.Json;
using System.Reflection;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerApp.Models;
using ServerApp.Models.BindingTargets;

namespace ServerApp.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductValuesController: Controller
    {
        private StoreContext context;

        private IProductRepository repository;

        public ProductValuesController(StoreContext _context, IProductRepository _repository)
        {
            context = _context;
            repository = _repository;
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(long id)
        {
            try
            {
                Product product = repository.GetProduct(id);
                return Ok(product);
            }
            catch (ProductNotFound e)
            {
                return StatusCode(405, e.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductData productData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    long productId = repository.AddProduct(productData.Product);
                    return Ok(productId);
                }
                catch (CategoryNotFound e)
                {
                    return StatusCode(405, e.Message);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult ReplaceProduct(long id, [FromBody] ProductDataBase productData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    repository.ReplaceProduct(id, productData.Product);
                    return Ok();
                }
                catch (ProductNotFound e)
                {
                    return StatusCode(405, e.Message);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPut("description/{productId}")]
        public IActionResult ReplaceDescription(long productId, [FromBody][Required][MaxLength(5000)] string value)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    repository.ReplaceDescription(productId, value);
                }
                catch (ProductNotFound e)
                {
                    return StatusCode(405, e.Message);
                }
                catch (DescriptionNotFound e)
                {
                    return StatusCode(405, e.Message);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateProduct(long id, 
            [FromBody] JsonPatchDocument<ProductData> patch)
        {
            Product product = context.Products
                .FirstOrDefault(p => p.Id == id);
            ProductData productData = new ProductData() { Product = product };
            
            patch.ApplyTo(productData);
            
            if (ModelState.IsValid && TryValidateModel(productData))
            {
                context.SaveChanges();
                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(long id)
        {
            repository.DeleteProduct(id);
            return Ok();
        }

        [HttpDelete("bycategory/{categoryId}")]
        public IActionResult DeleteProducts(long categoryId)
        {
            repository.DeleteProductsByCategory(categoryId);
            return Ok();
        }

        [HttpDelete("bygroup/{groupId}")]
        public IActionResult DeleteGroup(long groupId)
        {
            repository.DeleteGroup(groupId);
            return Ok();
        }
        
        [HttpGet]
        public IEnumerable<Product> GetProducts(int? pageSize = null, int? pageNumber = null, 
            string search = null, long? categoryId = null, bool? inStock = null, 
            decimal? minPrice = null, decimal? maxPrice = null)
        {
            return repository.GetFilteredProducts(
                pageSize, pageNumber, search, categoryId, 
                inStock, minPrice, maxPrice);
        }

        [HttpPost("filter")]
        public IEnumerable<Product> GetProducts([FromBody] SearchLines searchByProperty, 
            int? pageSize = null, int? pageNumber = null, string search = null, 
            long? categoryId = null, bool? inStock = null, 
            decimal? minPrice = null, decimal? maxPrice = null)
        {
            return repository.GetFilteredProducts(
                pageSize, pageNumber, search, categoryId, 
                inStock, minPrice, maxPrice, searchByProperty);
        }
    }
}