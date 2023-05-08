using Common;
using MethodTimer;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly ILogger<PeopleController> _logger;

        public PeopleController(ILogger<PeopleController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Time]
        public async Task<IResult> GetPeople([FromQuery] int quantity = 5000)
        {
            var response = await MockDataContext.GetMockPeople(quantity);

            return Results.Ok(response);
        }
    }
}