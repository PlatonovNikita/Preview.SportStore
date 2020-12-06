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
        
        public ProductValuesController(StoreContext _context)
            => context = _context;

        [HttpGet("{id}")]
        public Product GetProduct(long id)
        {
            var product = context.Products
                .Include(p => p.Category)
                .ThenInclude(c => c.GroupProperties)
                .ThenInclude(gp => gp.Properties)
                .Include(p => p.GroupsValues)
                .ThenInclude(gv => gv.DoubleProps)
                .Include(p => p.GroupsValues)
                .ThenInclude(gv => gv.StrProps)
                .Include(p => p.GroupsValues)
                .ThenInclude(gv => gv.BoolProps)
                .First(p => p.Id == id);
            if (product.Category != null)
            {
                product.Category.Products = null;
                product.Category.GroupProperties = null;
            }
            if (product.GroupsValues != null)
            {
                foreach (var groupValuese in product.GroupsValues)
                {
                    if (groupValuese.GroupProperty != null)
                    {
                        groupValuese.GroupProperty.Properties = null;
                    }
                }
            }

            return product;
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductData productData)
        {
            if (ModelState.IsValid)
            {
                if (context.Categories
                    .Any(c => c.Id == productData.CategoryId))
                {
                    var groupProperties = context.Set<GroupProperty>()
                        .Where(gp => gp.CategoryId == productData.CategoryId);
                    var groupVal = new List<GroupValues>();
                    foreach (var gp in groupProperties)
                    {
                        groupVal.Add(new GroupValues {GroupPropertyId = gp.Id});
                    }
                    
                    Product p = productData.Product;
                    p.GroupsValues = groupVal;
                    context.Add(p);
                    context.SaveChanges();
                    return Ok(p.Id);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult ReplaceProduct(long id, [FromBody] ProductData productData)
        {
            if (ModelState.IsValid)
            {
                var prod = context.Products.FirstOrDefault(p => p.Id == id);
                if (prod == null) return Problem("This id is not valid");
                
                Product p = productData.Product;
                prod.Name = p.Name;
                prod.Description = p.Description;
                prod.Price = p.Price;
                prod.InStock = p.InStock;
                context.SaveChanges();
                return Ok();
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
        public void DeleteProduct(long id)
        {
            context.Products.Remove(new Product {Id = id});
            context.SaveChanges();
        }

        [HttpDelete("bycategory/{categoryId}")]
        public void DeleteProducts(long categoryId)
        {
            var products = context.Products
                .Where(p => p.CategoryId == categoryId);

            if (products != null)
            {
                context.Products.RemoveRange(products);
                context.SaveChanges();
            }
            
        }

        [HttpDelete("bygroup/{groupId}")]
        public void DeleteGroup(long groupId)
        {
            var group = context.Set<GroupValues>()
                .Where(gv => gv.GroupPropertyId == groupId);
            context.RemoveRange(group);
            context.SaveChanges();
        }
        
        [HttpGet]
        public IEnumerable<Product> GetProducts(int? pageSize = null, int? pageNumber = null, 
            string search = null, long? categoryId = null, bool? inStock = null, 
            decimal? minPrice = null, decimal? maxPrice = null)
        {
            IQueryable<Product> query = GetQuery(search, 
                categoryId, inStock, minPrice, maxPrice);
            
            pageSize ??= 4;
            pageNumber ??= 1;

            return query.Skip((int)((pageNumber - 1) * pageSize)).Take((int)pageSize);
        }

        [HttpPost("filter")]
        public IEnumerable<Product> GetProducts([FromBody] SearchLines searchByProperty, 
            int? pageSize = null, int? pageNumber = null, string search = null, 
            long? categoryId = null, bool? inStock = null, 
            decimal? minPrice = null, decimal? maxPrice = null)
        {
            IQueryable<Product> query = GetQuery(search, 
                categoryId, inStock, minPrice, maxPrice);

            if (searchByProperty != null)
            {
                var doubleSearches = searchByProperty.DSearch;
                if (doubleSearches != null)
                {
                    foreach (var dSearch in doubleSearches)
                    {
                        query = query.Where(p => p
                            .GroupsValues
                            .Any(gv => gv
                                .DoubleProps
                                .Any(i => i.PropertyId == dSearch.PropertyId
                                          && (dSearch.Min == null || i.Value >= dSearch.Min)
                                          && (dSearch.Max == null || i.Value <= dSearch.Max)
                                )
                            )
                        );
                    }
                }

                var boolSearches = searchByProperty.BSearch;
                if (boolSearches != null)
                {
                    foreach (var bLine in boolSearches)
                    {
                        query = query.Where(p => p
                            .GroupsValues
                            .Any(gv => gv
                                .BoolProps
                                .Any(b => b.PropertyId == bLine.PropertyId
                                          && (b.Value == bLine.Value)
                                )
                            )
                        );
                    }
                }
            }

            pageSize ??= 4;
            pageNumber ??= 1;

            return query.Skip((int)((pageNumber - 1) * pageSize)).Take((int)pageSize);
        }

        [NonAction]
        private IQueryable<Product> GetQuery(string search = null, 
            long? categoryId = null, bool? inStock = null,
            decimal? minPrice = null, decimal? maxPrice = null)
        {
            IQueryable<Product> query = context.Products;
            if (inStock != null)
            {
                query = query.Where(p => p.InStock == inStock);
            }

            if (minPrice != null)
            {
                query = query.Where(p => p.Price >= minPrice);
            }

            if (maxPrice != null)
            {
                query = query.Where(p => p.Price <= maxPrice);
            }

            if (categoryId != null)
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                string lowerSearch = search.ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(lowerSearch));
                if (categoryId == null)
                {
                    query = query.Where(p => p.Category.Name.ToLower().Contains(lowerSearch));
                }
            }

            return query;
        }
    }
}