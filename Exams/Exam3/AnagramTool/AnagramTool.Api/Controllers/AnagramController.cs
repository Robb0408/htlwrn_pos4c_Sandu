using AnagramTool.Api.Models;
using AnagramTool.Logic.Service;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace AnagramTool.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class AnagramController : ControllerBase
{
    private readonly IAnagramService service;

    public AnagramController(IAnagramService service)
    {
        this.service = service;
    }

    /// <summary>
    /// Checks if a word is an anagram to another
    /// </summary>
    /// <param name="anagramDto">Object that contains 2 words</param>
    /// <response code="200">True if anagram, false if not</response>
    /// <response code="400">Given data is invalid</response>
    [HttpPost("check")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CheckAnagram([FromBody] AnagramDto anagramDto)
    {
        if (Regex.IsMatch(anagramDto.Word1, @"^[a-zA-Z]+$") && Regex.IsMatch(anagramDto.Word2, @"^[a-zA-Z]+$"))
        {
            return Ok(service.CheckAnagram(anagramDto.Word1, anagramDto.Word2));
        }
        return BadRequest("The given data is not valid.");
        
    }

    /// <summary>
    /// Finds all anagrams for a word from URL query
    /// </summary>
    /// <param name="word">Word to use as search base</param>
    /// <response code="200">All found anagrams</response>
    /// <response code="404">No anagrams found</response>
    [HttpGet("find")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<string>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FindAnagramFromQuery([FromQuery] string word)
    {
        var result = await service.FindAnagramsAsync(word);
        // HAU: ℹ️ you could also use !Any() 
        if (result.Count() == 0)
        {
            return NotFound();
        }
        return Ok(result);
    }

    /// <summary>
    /// Finds all anagrams for a word from route
    /// </summary>
    /// <param name="word">Word to use as search base</param>
    /// <response code="200">All found anagrams</response>
    /// <response code="404">No anagrams found</response>
    [HttpGet("search/{word}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<string>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FindAnagramFromRouteAsync(string word)
    {
        var result = await service.FindAnagramsAsync(word);
        // HAU: ℹ️ you could also use !Any()
        if (result.Count() == 0)
        {
            return NotFound();
        }
        return Ok(result);
    }
}