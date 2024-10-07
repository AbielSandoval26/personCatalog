using Application.Commands;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace personCatalogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var persons = await _mediator.Send(new GetAllPersonsQuery());
            return Ok(persons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonByID(int id)
        {
            var person = await _mediator.Send(new GetPersonByIdQuery(id));
            if (person == null)
                return NotFound();

            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePersonCommand command)
        {
            var personId = await _mediator.Send(command);
            return Ok(personId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson (int id, UpdatePersonCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var result = await _mediator.Send(command);

            if (result <= 0)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson (int id)
        {
            var result = await _mediator.Send(new DeletePersonCommand(id));

            if (result <= 0)
                return NotFound();

            return NoContent();

        }

    }
}
