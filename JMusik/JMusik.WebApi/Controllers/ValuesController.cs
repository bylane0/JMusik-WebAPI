using Microsoft.AspNetCore.Mvc;

namespace JMusik.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ValuesController : ControllerBase
    {
        //GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }
        //GET api/values/3
        [HttpGet("{ID}")]
        public ActionResult<string> Get(int ID)
        {
            return "value";
        }
        //POST api/values

        [HttpPost]
        public void Post([FromBody] string value)
        {
            ;
        }
        //PUT api/values/2

        [HttpPut("{ID}")]
        public void Put(int id, [FromBody] string value)
        {
            ;
        }
        //DELETE api/values/1
        [HttpDelete("{ID}")]
        public void Delete(int id)
        {
            ;
        }
    }
}
