using Gatherly.Domain.Errors;
using Gatherly.Domain.primitives;
using Gatherly.Domain.Shared;

namespace Gatherly.Domain.ValueObjects;

public class Email : ValueObject
{
    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Email> Create(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return Result.Failure<Email>(ValidationErrors.Email.Empty);
        }

        return new Email(email);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
