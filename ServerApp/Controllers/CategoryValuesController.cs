using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerApp.Models;
using ServerApp.Models.BindingTargets;

namespace ServerApp.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryValuesController : Controller
    {
        private StoreContext context;

        public CategoryValuesController(StoreContext _context)
            => context = _context;

        [HttpGet("{id}")]
        public Category GetCategory(long id)
        {
            return context.Categories
                .Include(c => c.GroupProperties)
                .ThenInclude(gp => gp.Properties)
                .FirstOrDefault(c => c.Id == id);
        }

        [HttpGet]
        public IEnumerable<Category> GetCategories()
        {
            return context.Categories
                .Include(c => c.GroupProperties)
                .ThenInclude(gp => gp.Properties);
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryData catData)
        {
            if (ModelState.IsValid)
            {
                Category cat = catData.Category;
                context.Categories.Add(cat);
                context.SaveChanges();
                return Ok(cat.Id);
            }

            return BadRequest(ModelState);
        }


        [HttpPut("{id}")]
        public IActionResult ReplaceCategory(long id, [FromBody][Required] CategoryData catData)
        {
            if (ModelState.IsValid)
            {
                Category cat = context.Categories
                    .FirstOrDefault(c => c.Id == id);
                if (cat == null) return Problem("This id is not valid");

                cat.Name = catData.Name;
                cat.NikName = catData.NikName;
                context.SaveChanges();
                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(long id)
        {
            try
            {
                context.Categories.Remove(new Category {Id = id});
                context.SaveChanges();
                return Ok();
            }
            catch (DbUpdateException e)
            {
                return BadRequest("There are product that dependencies from this category!");
            }
            
        }
        
        [HttpPost("group")]
        public IActionResult CreatePropertieGroup([FromBody] GroupPropertiesData gpData)
        {
            if (ModelState.IsValid)
            {
                if (context.Categories.Any(c => c.Id == gpData.CategoryId))
                {
                    var groupProps = gpData.GroupProperty;
                    context.Add(groupProps);
                    context.SaveChanges();
                    return Ok(groupProps.Id);
                }

                return Problem("Category id is not valid");
            }

            return BadRequest(ModelState);
        }

        [HttpPut("group/{id}")]
        public IActionResult ReplaceGroup(long id, [FromBody] [Required] string name)
        {
            if (ModelState.IsValid)
            {
                GroupProperty groupProp = context.Set<GroupProperty>()
                    .FirstOrDefault(gp => gp.Id == id);
                if (groupProp == null) return Problem("This id is nod valid");

                groupProp.Name = name;
                context.SaveChanges();
                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("group/{id}")]
        public IActionResult DeleteGroup(long id)
        {
            try
            {
                context.Set<GroupProperty>()
                    .Remove(new GroupProperty {Id = id});
                context.SaveChanges();
                return Ok();
            }
            catch (DbUpdateException e)
            {
                return BadRequest("There are products that contains group " +
                                  "values dependencies from this group!");
            }
        }
        
        [HttpPost("property")]
        public IActionResult CreateProperty([FromBody] PropertyData propData)
        {
            if (ModelState.IsValid)
            {
                if (context.Set<GroupProperty>()
                    .Any(gp => gp.Id == propData.GroupPropertyId))
                {
                    var property = propData.Property;
                    context.Add(property);
                    context.SaveChanges();
                    return Ok(property.Id);
                }

                return Problem("Group id is not valid");
            }

            return BadRequest(ModelState);
        }

        [HttpPut("property/{id}")]
        public IActionResult ReplaceProperty(long id, [FromBody] PropertyDataBase propData)
        {
            if (ModelState.IsValid)
            {
                Property prop = context.Set<Property>()
                    .FirstOrDefault(p => p.Id == id);
                if (prop == null) return Problem("This id is not valid");

                prop.Name = propData.Name;
                prop.PropType = (PropertyType) propData.PropertyType;
                context.SaveChanges();
                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("property/{id}")]
        public IActionResult DeleteProperty(long id)
        {
            try
            {
                context.Set<Property>()
                    .Remove(new Property {Id = id});
                context.SaveChanges();
                return Ok();
            }
            catch (DbUpdateException e)
            {
                return BadRequest("There are products that contains property " +
                                  "value dependencies from this property!");
            }
        }
    }
}