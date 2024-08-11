using Gatherly.Api.Abstractions;
using Gatherly.Application.Members.Commands.CreateMember;
using Gatherly.Application.Members.Queries.GetMemberById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gatherly.Api.Controllers;


[Route("api/[controller]")]
public class MembersController(ISender sender) : ApiController(sender)
{
    [HttpPost]
    public async Task<IActionResult> RegisterMember(CancellationToken cancellationToken = default)
    {
        var command = new CreateMemberCommand(
            "Test_First",
            "Last",
            "first.last@test.com");

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : BadRequest(result.Errors);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetMemberDetails([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var query = new GetMemberByIdQuery(id);

        var response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }
}
