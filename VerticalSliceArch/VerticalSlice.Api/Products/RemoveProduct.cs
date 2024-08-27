using Microsoft.AspNetCore.Http.HttpResults;
using VerticalSlice.Api.Endpoints;
using VerticalSlice.Api.Products.Data;

namespace VerticalSlice.Api.Products;

public static class RemoveProduct
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("products/{id}", Handler).WithTags("Products");
        }
    }

    public static async Task<Results<NoContent, NotFound>> Handler(int id, AppDbContext context)
    {
        var product = await context.Products.FindAsync(id);

        if (product is null)
        {
            return TypedResults.NotFound();
        }

        context.Remove(product);

        await context.SaveChangesAsync();

        return TypedResults.NoContent();
    }
}
