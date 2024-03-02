using Microsoft.AspNetCore.Mvc;
using CollatzConjecture.Logic;

namespace CollatzConjecture.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StringController : ControllerBase
    {
        private ICollatzConjectureService service;

        public StringController(ICollatzConjectureService service)
        {
            this.service = service;
        }

        [HttpGet("{number}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetSequence(string number)
        {
            var result = service.GetSequence(number);
            if (result.Count == 0)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet("valid/{number}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<string>))]
        public IActionResult IsValid(string number)
        {
            var result = service.IsSequenceValid(number);
            return Ok(result);
        }
    }
}
