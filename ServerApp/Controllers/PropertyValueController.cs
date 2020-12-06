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

        public PropertyValueController(StoreContext _context)
            => context = _context;
        
        [HttpPost("dproperty")]
        public IActionResult CreatePropertyInt([FromBody] DoubleLineData doubleData)
        {
            if (ModelState.IsValid)
            {
                var check = CheckValidProperty(doubleData);
                if (check != null) return check;

                var groupValuesId = context.Set<GroupValues>()
                    .First(gv => gv.GroupPropertyId == doubleData.GroupPropertyId
                                 && gv.ProductId == doubleData.ProductId)
                    .Id;
                var intProp = doubleData.DoubleLine;
                intProp.GroupValuesId = groupValuesId;
                context.Add(intProp);
                context.SaveChanges();
                return Ok(intProp.Id);
            }

            return BadRequest(ModelState);
        }
        
        [HttpPut("dproperty/{id}")]
        public IActionResult ReplacePropertyInt(long id, [FromBody][Required] double value)
        {
            if (ModelState.IsValid)
            {
                var intProp = context.Set<DoubleLine>()
                    .FirstOrDefault(i => i.Id == id);
                if (intProp == null) return Problem("Id is not valid");
                
                intProp.Value = value;
                context.Update(intProp);
                context.SaveChanges();
                return Ok();
            }

            return BadRequest(ModelState);
        }
        
        [HttpPost("bproperty")]
        public IActionResult CreatePropertyBool([FromBody] BoolLineData boolData)
        {
            if (ModelState.IsValid)
            {
                var check = CheckValidProperty(boolData);
                if (check != null) return check;
                
                var groupValuesId = context.Set<GroupValues>()
                    .First(gv => gv.GroupPropertyId == boolData.GroupPropertyId 
                                 && gv.ProductId == boolData.ProductId)
                    .Id;
                var boolProp = boolData.BoolLine;
                boolProp.GroupValuesId = groupValuesId;
                context.Add(boolProp);
                context.SaveChanges();
                return Ok(boolProp.Id);
            }

            return BadRequest(ModelState);
        }
        
        [HttpPut("bproperty/{id}")]
        public IActionResult ReplacePropertyBool(long id, [FromBody][Required] bool value)
        {
            if (ModelState.IsValid)
            {
                var boolProp = context.Set<BoolLine>()
                    .FirstOrDefault(i => i.Id == id);
                if (boolProp == null) return Problem("Id is not valid");
                
                boolProp.Value = value;
                context.Update(boolProp);
                context.SaveChanges();
                return Ok();
            }

            return BadRequest(ModelState);
        }
        
        [HttpPost("sproperty")]
        public IActionResult CreatePropertyStr([FromBody] StrLineData strData)
        {
            if (ModelState.IsValid)
            {
                var check = CheckValidProperty(strData);
                if (check != null) return check;
                
                var groupValuesId = context.Set<GroupValues>()
                    .First(gv => gv.GroupPropertyId == strData.GroupPropertyId 
                                 && gv.ProductId == strData.ProductId)
                    .Id;
                var strProp = strData.StrLine;
                strProp.GroupValuesId = groupValuesId;
                context.Add(strProp);
                context.SaveChanges();
                return Ok(strProp.Id);
            }

            return BadRequest(ModelState);
        }
        
        [HttpPut("sproperty/{id}")]
        public IActionResult ReplacePropertyStr(long id, [FromBody][Required] string value)
        {
            if (ModelState.IsValid)
            {
                var strProp = context.Set<StrLine>()
                    .FirstOrDefault(i => i.Id == id);
                if (strProp == null) return Problem("Id is not valid");
                
                strProp.Value = value;
                context.Update(strProp);
                context.SaveChanges();
                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("byproperty/{propertyId}")]
        public void DeleteProperty(long propertyId)
        {
            var dl = context.Set<DoubleLine>().
                Where(d => d.PropertyId == propertyId);
            var bl = context.Set<BoolLine>()
                .Where(b => b.PropertyId == propertyId);
            var sl = context.Set<StrLine>()
                .Where(s => s.PropertyId == propertyId);
            
            context.RemoveRange(bl);
            context.RemoveRange(dl);
            context.RemoveRange(sl);
            context.SaveChanges();
        }

        [NonAction]
        public IActionResult CheckValidProperty(BaseLineData data)
        {
            if (!context.Set<GroupProperty>()
                .Any(gv => gv.Id == data.GroupPropertyId))
            {
                return Problem("Group id is not valid");
            }

            if (!context.Products.Any(p => p.Id == data.ProductId))
            {
                return Problem("Product id is not valid");
            }
                
            if (!context.Set<Property>()
                .Any(p => p.Id == data.PropertyId))
            {
                return Problem("Property id is not valid");
            }

            return null;
        }
    }
}