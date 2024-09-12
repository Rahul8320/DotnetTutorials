using VerticalSlice.Api.Endpoints;

namespace VerticalSlice.Api.Users;

internal sealed class UserEndpoints : IEndpoint
{
    private const string Tag = "Users";
    public const string VerifyEmail = "VerifyEmail";

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/register", async (
            RegisterUser.Request request,
            RegisterUser useCase,
            CancellationToken cancellationToken)
            => await useCase.Handle(request, cancellationToken)).WithTags(Tag);

        app.MapPost("users/login", async (
            LoginUser.Request request,
            LoginUser useCase)
            => await useCase.Handle(request)).WithTags(Tag);

        app.MapGet("users/verify-email", async (Guid tokenId, VerifyEmail useCase)
            => await useCase.Handle(tokenId)).WithTags(Tag).WithName(VerifyEmail);
    }
}
