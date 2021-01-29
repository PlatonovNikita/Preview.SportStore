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

        private ICategoryRepository repository;

        public CategoryValuesController(StoreContext _context, ICategoryRepository _repository)
        {
            context = _context;
            repository = _repository;
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(long id)
        {
            try
            {
                var category = repository.GetCategory(id);
                return Ok(category);
            }
            catch (CategoryNotFound e)
            {
                return StatusCode(405, e.Message);
            }
        }

        [HttpGet]
        public IEnumerable<Category> GetCategories(string search = null)
        {
            return repository.GetFilteredCategories(search);
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryData catData)
        {
            if (ModelState.IsValid)
            {
                var categoryId = repository.AddCategory(catData.Category);
                return Ok(categoryId);
            }
            return BadRequest(ModelState);
        }


        [HttpPut("{id}")]
        public IActionResult ReplaceCategory(long id, [FromBody][Required] CategoryData catData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    repository.UpdateCategory(id, catData.Category);
                    return Ok();
                }
                catch (CategoryNotFound e)
                {
                    return StatusCode(405, e.Message);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(long id)
        {
            try
            {
                repository.DeleteCategory(id);
                return Ok();
            }
            catch (DbUpdateException)
            {
                return StatusCode(405,"There are products that dependencies from this category!");
            }
        }
        
        [HttpPost("group")]
        public IActionResult CreatePropertyGroup([FromBody] GroupPropertiesData gpData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var propertyGroupId = repository.AddPropertyGroup(gpData.GroupProperty);
                    return Ok(propertyGroupId);
                }
                catch (CategoryNotFound e)
                {
                    return StatusCode(405, e.Message);
                }
                catch (UnacceptableNameGroup e)
                {
                    return StatusCode(405, e.Message);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPut("group/{id}")]
        public IActionResult ReplaceGroup(long id, [FromBody] [Required] string name)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    repository.UpdateGroup(id, new GroupProperty {Name = name});
                    return Ok();
                }
                catch (GroupCategoryNotFound e)
                {
                    return StatusCode(405, e.Message);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("group/{id}")]
        public IActionResult DeleteGroup(long id)
        {
            try
            {
                repository.DeleteGroup(id);
                return Ok();
            }
            catch (DbUpdateException)
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
                try
                {
                    var propertyId = repository.AddProperty(propData.Property);
                    return Ok(propertyId);
                }
                catch (GroupCategoryNotFound e)
                {
                    return StatusCode(405, e.Message);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPut("property/{id}")]
        public IActionResult ReplaceProperty(long id, [FromBody] PropertyDataBase propData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    repository.UpdateProperty(id, propData.Property);
                    return Ok();
                }
                catch (PropertyNotFound e)
                {
                    return StatusCode(405, e.Message);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("property/{id}")]
        public IActionResult DeleteProperty(long id)
        {
            try
            {
                repository.DeleteProperty(id);
                return Ok();
            }
            catch (DbUpdateException)
            {
                return BadRequest("There are products that contains property " +
                                  "value dependencies from this property!");
            }
        }

        [HttpGet("uniquestrings/{propertyId}")]
        public IActionResult GetUniqueStrings(long propertyId)
        {
            try
            {
                return Ok(repository.GetUniqueStrings(propertyId));
            }
            catch (CategoryNotFound e)
            {
                return StatusCode(405, e.Message);
            }
        }
    }
}