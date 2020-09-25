using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Resporter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("{value}")]
        public ActionResult<string> GetSquareValue(string value)
        {
            int id;
            if (int.TryParse(value, out id))
                return (id * id).ToString();
            else
                return BadRequest();
        }

        [HttpGet()]
        public ActionResult<IEnumerable<string>> GetValues()
        {
            return new string[] { "value1", "value2", "value3" };
        }
    }
}