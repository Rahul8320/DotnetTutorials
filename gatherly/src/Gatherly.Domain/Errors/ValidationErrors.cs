using Gatherly.Domain.Constants;
using Gatherly.Domain.Shared;

namespace Gatherly.Domain.Errors;

public static class ValidationErrors
{
    public static class FirstName
    {
        public static readonly Error Empty = new(
            "FirstName.Empty",
            "First name can not be empty.");

        public static readonly Error TooLong = new(
            "FirstName.TooLong",
            $"First name can not be more than {DomainConstants.FirstNameMaxLength} character long.");
    }

    public static class LastName
    {
        public static readonly Error Empty = new(
            "LastName.Empty",
            "Last name can not be empty.");

        public static readonly Error TooLong = new(
            "LastName.TooLong",
            $"Last name can not be more than {DomainConstants.LastNameMaxLength} character long.");
    }
}
