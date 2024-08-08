using Gatherly.Domain.primitives;
using Gatherly.Domain.ValueObjects;

namespace Gatherly.Domain.Entities;

public sealed class Member : Entity
{
    public Member(Guid id, FirstName firstName, LastName lastName, string email) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public FirstName FirstName { get; set; }

    public LastName LastName { get; set; }

    public string Email { get; set; }
}