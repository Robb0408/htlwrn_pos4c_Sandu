using CollatzConjecture.Logic;
using Microsoft.AspNetCore.Mvc;

namespace CollatzConjecture.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NumberController : ControllerBase
    {
        private ICollatzConjectureService service;

        public NumberController(ICollatzConjectureService service)
        {
            this.service = service;
        }

        [HttpGet("{number}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetSequence(int number)
        {
            var result = service.GetSequence(number);
            return Ok(result);
        }

        [HttpGet("valid/{number}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<int>))]
        public IActionResult IsValid(int number)
        {
            var result = service.IsSequenceValid(number);
            return Ok(result);
        }

    }
}
