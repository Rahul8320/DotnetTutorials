using Gatherly.Domain.Constants;
using Gatherly.Domain.Errors;
using Gatherly.Domain.primitives;
using Gatherly.Domain.Shared;

namespace Gatherly.Domain.ValueObjects;

public sealed class LastName : ValueObject
{
    private LastName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<LastName> Create(string lastName)
    {
        if (string.IsNullOrEmpty(lastName) == true)
        {
            return Result.Failure<LastName>(ValidationErrors.LastName.Empty);
        }

        if (lastName.Length > DomainConstants.LastNameMaxLength)
        {
            return Result.Failure<LastName>(ValidationErrors.LastName.TooLong);
        }

        return new LastName(lastName);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
