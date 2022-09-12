using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        DataAccessLayer dal = new DataAccessLayer();

        [HttpGet]
        public List<string[]> GetAllEvents()
        {
            return dal.Get();
        }

        [HttpPost]
        public string Post([FromBody] Object jsonFormat)
        {
            dal.Create(jsonFormat);

            return "Event added to the database!";
        }

        [HttpDelete]
        public string Delete([FromBody] string id)
        {        
            return dal.Delete(id);
        }

    }
}
