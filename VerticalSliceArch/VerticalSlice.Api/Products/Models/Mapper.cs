using FluentValidation.Results;
using VerticalSlice.Api.Products.Entity;

namespace VerticalSlice.Api.Products.Models;

public static class Mapper
{
    public static ProductResponse ToModel(this Product product)
    {
        return new ProductResponse(Id: product.Id, Name: product.Name, Price: product.Price);
    }

    public static List<ProductResponse> ToModels(this List<Product> products)
    {
        return products.Select(product =>
            new ProductResponse(Id: product.Id, Name: product.Name, Price: product.Price)).ToList();
    }

    public static List<ValidationError> ToValidationErrors(this List<ValidationFailure> errors)
    {
        return errors.Select(error => new ValidationError(
            Code: error.ErrorCode,
            Field: error.PropertyName,
            Message: error.ErrorMessage)).ToList();
    }
}