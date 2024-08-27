using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using VerticalSlice.Api.Data;
using VerticalSlice.Api.Endpoints;
using VerticalSlice.Api.Products.Models;

namespace VerticalSlice.Api.Products;

public static class GetAllProducts
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("products", Handler).WithTags("Products");
        }
    }

    public static async Task<Results<Ok<List<ProductResponse>>, NotFound>> Handler(AppDbContext context)
    {
        var products = await context.Products.ToListAsync();

        if (products.Count == 0)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(products.ToModels());
    }
}
