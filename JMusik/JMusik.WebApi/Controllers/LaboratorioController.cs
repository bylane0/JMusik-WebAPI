using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JMusik.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaboratorioController : ControllerBase
    {
        // GET: api/<LaboratorioController>
        [HttpGet]
        public string Get()
        {
            return "test";
        }

        // GET api/<LaboratorioController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LaboratorioController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LaboratorioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LaboratorioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
