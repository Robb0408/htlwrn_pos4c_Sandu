using FileContentAnalyzer.Logic;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FileContentAnalyzer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileAnalyzerController : ControllerBase
    {
        private IFileContentAnalyzerService service;

        public FileAnalyzerController(IFileContentAnalyzerService service)
        {
            this.service = service;
        }

        [HttpPost("wordfrequency")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDictionary<string, int>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetWordFrequency([FromBody] string content)
        {
            var result = service.GetFrequency(content);
            return Ok(result);
        }

        [HttpPost("uniquewords")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetUniqueWordsCount([FromBody] string content)
        {
            var result = service.GetUniqueWordsCount(content);
            return Ok(new
            {
                UniqueWordsCount = result
            });
        }

        [HttpPost("longestwords")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetLongestWords([FromBody] string content)
        {
            var result = service.GetLongestWords(content);
            return Ok(result);
        }

    }
}
