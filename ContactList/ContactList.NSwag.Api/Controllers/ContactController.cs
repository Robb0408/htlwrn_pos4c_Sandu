using ContactList.Logic.Models;
using ContactList.Logic.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace ContactList.NSwag.Api.Controllers
{
    [ApiController]
    [Route("")]
    public class ContactController : ControllerBase
    {
        private IContactService service;

        public ContactController(IContactService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Get all people in contact list
        /// </summary>
        /// <response code="200">Successful operation</response>
        [HttpGet("contacts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Person>))]
        public IActionResult GetContacts()
        {
            return Ok(service.GetContacts());
        }

        /// <summary>
        /// Adds a new person to the list of contacts
        /// </summary>
        /// <param name="person">Person to add</param>
        /// <response code="201">Person successfully created</response>
        /// <response code="400">Invalid input (e.g. required field missing or empty)</response>
        [HttpPost("contacts")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Person))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddContact([FromBody, Required] Person person)
        {
            var newPerson = service.AddContact(person);

            return CreatedAtAction(nameof(GetContacts), new { id = newPerson.Id }, newPerson);
        }

        /// <summary>
        /// Deletes a person from the list of contacts
        /// </summary>
        /// <param name="personId">ID of person to delete</param>
        /// <response code="204">Successful operation</response>
        /// <response code="400">Invalid ID supplied</response>
        /// <response code="404">Person not found</response>
        [HttpDelete("contacts/{personId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteContact(int personId)
        {
            var result = service.DeleteContact(personId);
            return result ? NoContent() : NotFound();
        }

        /// <summary>
        /// Finds person in concact list by name
        /// </summary>
        /// <remarks>
        /// Returns all people whose first of last name contains the specified filter
        /// </remarks>
        /// <param name="nameFilter"></param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid or missing name</response>
        [HttpGet("contacts/findByName")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Person))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetContact([FromQuery, Required] string nameFilter)
        {
            var result = service.GetContact(nameFilter);
            return result is not null ? Ok(result) : BadRequest();
        }
    }
}
