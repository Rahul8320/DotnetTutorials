using Gatherly.Domain.primitives;
using Gatherly.Domain.Shared;
using Gatherly.Domain.ValueObjects;

namespace Gatherly.Domain.Entities;

public sealed class Member : Entity
{
    private Member(Guid id, FirstName firstName, LastName lastName, Email email) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public FirstName FirstName { get; set; }

    public LastName LastName { get; set; }

    public Email Email { get; set; }

    public static Result<Member> Create(Guid id, FirstName firstName, LastName lastName, Email email)
    {
        return new Member(id, firstName, lastName, email);
    }
}