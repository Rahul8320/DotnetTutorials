using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using VerticalSlice.Api.Endpoints;
using VerticalSlice.Api.Products.Data;
using VerticalSlice.Api.Products.Models;

namespace VerticalSlice.Api.Products;

public static class CreateProduct
{
    public record Request(string Name, double Price);

    public sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage("Product name can not be empty");

            RuleFor(r => r.Price).GreaterThan(0).WithMessage("Price must be a positive number");
        }
    }

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("products", Handler).WithTags("Products");
        }
    }

    public static async Task<Results<Ok<ProductResponse>, BadRequest<List<ValidationError>>>> Handler(
        Request request,
        AppDbContext context,
        IValidator<Request> validator)
    {
        var validationResults = await validator.ValidateAsync(request);

        if (validationResults.IsValid == false)
        {
            return TypedResults.BadRequest(validationResults.Errors.ToValidationErrors());
        }

        var product = new Product { Name = request.Name, Price = request.Price };

        context.Products.Add(product);

        await context.SaveChangesAsync();

        return TypedResults.Ok(product.ToModel());
    }
}
