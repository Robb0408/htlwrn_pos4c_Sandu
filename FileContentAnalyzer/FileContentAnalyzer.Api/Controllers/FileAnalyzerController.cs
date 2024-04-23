using FileContentAnalyzer.Api.Models;
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
        private ILogger<FileAnalyzerController> logger;

        public FileAnalyzerController(IFileContentAnalyzerService service, ILogger<FileAnalyzerController> logger)
        {
            this.service = service;
            this.logger = logger;
        }

        /// <summary>
        /// Counts the frequency of every word in a text
        /// </summary>
        /// <param name="content">Text to analyze</param>
        /// <response code="200">Words an their count</response>
        /// <response code="400">Invalid text</response>
        [HttpPost("wordfrequency")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDictionary<string, int>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetWordFrequency([FromBody] string content)
        {
            logger.LogInformation("Calcutaing word frequency from text: {content}", content);
            var result = service.GetFrequency(content);
            logger.LogInformation("Calculation successful");
            return Ok(result);
        }

        /// <summary>
        /// Counts the amount of unique words in a text
        /// </summary>
        /// <param name="content">Text to analyze</param>
        /// <response code="200">Unique words count</response>
        /// <response code="400">Invalid text</response>
        [HttpPost("uniquewords")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetUniqueWordsCount([FromBody] string content)
        {
            logger.LogInformation("Counting unique words from text: {content}", content);
            var result = service.GetUniqueWordsCount(content);
            logger.LogInformation("Counting successful");
            return Ok(new
            {
                UniqueWordsCount = result
            });
        }

        /// <summary>
        /// Finds the longest words in a text
        /// </summary>
        /// <param name="content">Text to analyze</param>
        /// <response code="200">List of longest words</response>
        /// <response code="400">Invalid text</response>
        [HttpPost("longestwords")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetLongestWords([FromBody] string content)
        {
            logger.LogInformation("Fetching longest words from text: {content}", content);
            var result = service.GetLongestWords(content);
            logger.LogInformation("Fetch successful");
            return Ok(result);
        }

        /// <summary>
        /// Calculates the similarity between two texts
        /// </summary>
        /// <remarks>Uses the cosine similarity calculation</remarks>
        /// <param name="text">Text object with two texts to compare</param>
        /// <response code="200">Similarity score</response>
        /// <response code="400">Invalid object</response>
        [HttpPost("similarity")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(double))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetSimilarity([FromBody] SimilarityText text)
        {
            logger.LogInformation("Calculating similarity from texts: {@text}", text);
            var result = service.GetSimilarity(text.Text1, text.Text2);
            logger.LogInformation("Calculation successful");
            return Ok(new
            {
                SimilarityScore = result
            });
        }
    }
}
