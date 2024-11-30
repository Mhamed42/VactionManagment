using Microsoft.AspNetCore.Mvc;
using VactionManagment.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VactionManagment.ApiContrrollers
{
    [Route("api/VacationPlanApi")]
    [ApiController]
    public class VacationPlanApiController : ControllerBase
    {
        private readonly VacationDbContext _vacationDb;

        public VacationPlanApiController(VacationDbContext vacationDb)
        {
            _vacationDb = vacationDb;
        }
       
        // GET: api/<VacationPlanApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<VacationPlanApiController>/5
        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            try
            {
                return Ok(_vacationDb.Employees.Where(x=>x.Name.Contains(name)).ToList());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

        // POST api/<VacationPlanApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<VacationPlanApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VacationPlanApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
