using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ServerApp.Models;
using ServerApp.Models.BindingTargets;

namespace ServerApp.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class PropertyValueController: Controller
    {
        private StoreContext context;

        private IProductRepository repository;

        public PropertyValueController(StoreContext _context, IProductRepository _repository)
        {
            context = _context;
            repository = _repository;
        }

        [HttpPost("dproperty")]
        public IActionResult CreatePropertyDouble([FromBody] DoubleLineData doubleData)
        {
            if (ModelState.IsValid)
            {
                CheckValidProperty(doubleData);

                long propertyId = repository.CreatePropertyDouble(doubleData);
                return Ok(propertyId);
            }
            return BadRequest(ModelState);
        }
        
        [HttpPut("dproperty/{id}")]
        public IActionResult ReplacePropertyDouble(long id, [FromBody][Required] double value)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    repository.ReplacePropertyDouble(id, value);
                    return Ok();
                }
                catch (RelatedPropertyNotFound e)
                {
                    return StatusCode(405, e.Message);
                }
            }

            return BadRequest(ModelState);
        }
        
        [HttpPost("bproperty")]
        public IActionResult CreatePropertyBool([FromBody] BoolLineData boolData)
        {
            if (ModelState.IsValid)
            {
                CheckValidProperty(boolData);

                long propertyId = repository.CreatePropertyBool(boolData);
                return Ok(propertyId);
            }
            return BadRequest(ModelState);
        }
        
        [HttpPut("bproperty/{id}")]
        public IActionResult ReplacePropertyBool(long id, [FromBody][Required] bool value)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    repository.ReplacePropertyBool(id, value);
                    return Ok();
                }
                catch (RelatedPropertyNotFound e)
                {
                    return StatusCode(405, e.Message);
                }
            }
            return BadRequest(ModelState);
        }
        
        [HttpPost("sproperty")]
        public IActionResult CreatePropertyStr([FromBody] StrLineData strData)
        {
            if (ModelState.IsValid)
            {
                CheckValidProperty(strData);

                long propertyId = repository.CreatePropertyStr(strData);
                return Ok(propertyId);
            }
            return BadRequest(ModelState);
        }
        
        [HttpPut("sproperty/{id}")]
        public IActionResult ReplacePropertyStr(long id, [FromBody][Required] string value)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    repository.ReplacePropertyStr(id, value);
                    return Ok();
                }
                catch (RelatedPropertyNotFound e)
                {
                    return StatusCode(405, e.Message);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("byproperty/{propertyId}")]
        public IActionResult DeleteProperty(long propertyId)
        {
            repository.DeleteProperty(propertyId);
            return Ok();
        }

        [NonAction]
        public IActionResult CheckValidProperty(BaseLineData data)
        {
            try
            {
                repository.CheckValidProperty(data);
            }
            catch (RelatedGroupNotFound e)
            {
                return StatusCode(405, e.Message);
            }
            catch (ProductNotFound e)
            {
                return StatusCode(405, e.Message);
            }
            catch (RelatedPropertyNotFound e)
            {
                return StatusCode(405, e.Message);
            }
            return null;
        }
    }
}