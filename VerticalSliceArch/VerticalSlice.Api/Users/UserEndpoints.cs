using VerticalSlice.Api.Endpoints;

namespace VerticalSlice.Api.Users;

public sealed class UserEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("register", async (
            RegisterUser.Request request,
            RegisterUser useCase,
            CancellationToken cancellationToken)
        => await useCase.Handle(request, cancellationToken)).WithTags("Users");

        app.MapPost("login", async (
            LoginUser.Request request,
            LoginUser useCase)
        => await useCase.Handle(request)).WithTags("Users");
    }
}
