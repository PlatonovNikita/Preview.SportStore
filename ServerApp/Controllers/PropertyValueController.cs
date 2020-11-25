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
                
                var intProp = doubleData.DoubleLine;
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
                
                var boolProp = boolData.BoolLine;
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
                
                var strProp = strData.StrLine;
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

        [NonAction]
        public IActionResult CheckValidProperty(BaseLineData data)
        {
            if (!context.Set<GroupValues>()
                .Any(gv => gv.Id == data.GroupValuesId))
            {
                return Problem("Group id is not valid");
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