using Gatherly.Domain.Constants;
using Gatherly.Domain.Errors;
using Gatherly.Domain.primitives;
using Gatherly.Domain.Shared;

namespace Gatherly.Domain.ValueObjects;

public sealed class FirstName : ValueObject
{
    private FirstName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<FirstName> Create(string firstName)
    {
        if (string.IsNullOrEmpty(firstName) == true)
        {
            return Result.Failure<FirstName>(ValidationErrors.FirstName.Empty);
        }

        if (firstName.Length > DomainConstants.FirstNameMaxLength)
        {
            return Result.Failure<FirstName>(ValidationErrors.FirstName.TooLong);
        }

        return new FirstName(firstName);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
