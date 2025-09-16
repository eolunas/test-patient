using COLAPP.Application.Features.Patient.Commands.Create;
using COLAPP.Application.Features.Patient.Commands.Delete;
using COLAPP.Application.Features.Patient.Commands.Update;
using COLAPP.Application.Features.Patient.Queries.GetAll;
using COLAPP.Application.Features.Patient.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace COLAPP.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var command = new GetAllPatientQuery();
        var data = await _sender.Send(command);
        return Ok(data);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var command = new GetByIdPatientQuery(id);
        var data = await _sender.Send(command);
        return data is null ? NotFound() : Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePatientCommand request)
    {
        var id = await _sender.Send(request);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePatientCommand command, CancellationToken ct)
    {
        if (id != command.Id) return BadRequest("El ID del body no coincide con el de la ruta");

        await _sender.Send(command, ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _sender.Send(new DeletePatientCommand(id));
        return NoContent();
    }

}
