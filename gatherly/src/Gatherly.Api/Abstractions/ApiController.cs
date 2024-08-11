using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gatherly.Api.Abstractions;

[ApiController]
public class ApiController(ISender sender) : ControllerBase
{
    protected readonly ISender Sender = sender;
}
