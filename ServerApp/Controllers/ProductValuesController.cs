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
                .ThenInclude(gv => gv.IntProps)
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

        [HttpGet]
        public IEnumerable<Product> GetProducts(string search = null, string category = null, 
            bool? inStock = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            //[FromBody]IEnumerable<IntLine> ints = null, [FromBody]IEnumerable<BoolLine> bools = null
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

            if (!string.IsNullOrWhiteSpace(search))
            {
                string lowerSearch = search.ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(lowerSearch));
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                string lowerCat = category.ToLower();
                query = query.Where(p => p.Category.Name.ToLower().Contains(lowerCat));
            }

            /*if (ints != null)
            {
                var dictInts = ints.ToDictionary(i => i.PropertyId);
                query = query.Where(p =>
                    p.GroupsValues.Any(gv =>
                        gv.IntProps.Any(i =>
                            dictInts.ContainsKey(i.PropertyId)
                            && i.Value == dictInts[i.PropertyId].Value)));
            }

            if (bools != null)
            {
                var dictBools = bools.ToDictionary(b => b.PropertyId);
                query = query.Where(p =>
                    p.GroupsValues.Any(gv =>
                        gv.BoolProps.Any(b =>
                            dictBools.ContainsKey(b.PropertyId)
                            && b.Value == dictBools[b.PropertyId].Value)));
            }*/

            return query;
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
                if (!context.Products.Any(p => p.Id == id))
                    return Problem("This id is not valid");
                
                Product p = productData.Product;
                p.Id = id;
                context.Update(p);
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
                if (productData.CategoryId != null
                    && !context.Categories.Any(c => c.Id == productData.CategoryId)) 
                    return Problem("This id is not valid");
                context.SaveChanges();
                return Ok();
            }

            return BadRequest(ModelState);
        }
    }
}