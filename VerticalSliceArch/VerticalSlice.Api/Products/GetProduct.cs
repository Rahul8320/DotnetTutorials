using Microsoft.AspNetCore.Http.HttpResults;
using VerticalSlice.Api.Endpoints;
using VerticalSlice.Api.Products.Data;
using VerticalSlice.Api.Products.Models;

namespace VerticalSlice.Api.Products;

public sealed class GetProduct
{
    public sealed class Endpoint : IEndpoint
    {
        void IEndpoint.MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("products/{id}", Handler).WithTags("Products");
        }
    }

    public static async Task<Results<Ok<ProductResponse>, NotFound>> Handler(int id, AppDbContext context)
    {
        var product = await context.Products.FindAsync(id);

        if (product is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(product.ToModel());
    }
}
