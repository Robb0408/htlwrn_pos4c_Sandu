using CollatzConjecture.Logic;
using Microsoft.AspNetCore.Mvc;

namespace CollatzConjecture.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class NumberController : ControllerBase
{
    private ICollatzConjectureService service;
    
    public NumberController(ICollatzConjectureService service)
    {
        this.service = service;
    }
    
    [HttpGet("/{number}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<int>))]
    public IActionResult GetSequence(int number)
    {
        var sequence = service.GetSequence(number);
        return Ok(sequence);
    }
    
    [HttpGet("/valid/{number}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    public IActionResult IsSequenceValid(int number)
    {
        var result = service.IsSequenceValid(number);
        return Ok(result);
    }
}