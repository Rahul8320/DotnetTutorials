using Gatherly.Domain.Enums;
using Gatherly.Domain.Errors;
using Gatherly.Domain.primitives;
using Gatherly.Domain.Shared;

namespace Gatherly.Domain.Entities;

public sealed class Gathering : AggregateRoot
{
    private readonly List<Invitation> _invitations = [];
    private readonly List<Attendee> _attendees = [];

    private Gathering(
    Guid id,
    Member creator,
    GatheringType gatheringType,
    DateTime scheduledAtUtc,
    string name,
    string? location) : base(id)
    {
        Creator = creator;
        Type = gatheringType;
        Name = name;
        Location = location;
        ScheduledAtUtc = scheduledAtUtc;
    }

    public Member Creator { get; private set; }

    public GatheringType Type { get; private set; }

    public string Name { get; private set; }

    public DateTime ScheduledAtUtc { get; private set; }

    public string? Location { get; private set; }

    public int? MaximumNumberOfAttendees { get; private set; }

    public DateTime? InvitationsExpireAtUtc { get; private set; }

    public int NumberOfAttendees { get; private set; }

    public IReadOnlyCollection<Attendee> Attendees => _attendees;

    public IReadOnlyCollection<Invitation> Invitations => _invitations;

    public static Result<Gathering> Create(
        Guid id,
        Member creator,
        GatheringType gatheringType,
        DateTime scheduledAtUtc,
        string name,
        string? location,
        int? maxNumberOfAttendees,
        int? invitationsValidBeforeInHours)
    {
        // Create gathering
        var gathering = new Gathering(
            id,
            creator,
            gatheringType,
            scheduledAtUtc,
            name,
            location);

        // Calculate gathering type details
        switch (gathering.Type)
        {
            case GatheringType.WithFixedNumberOfAttendees:
                if (maxNumberOfAttendees is null)
                {
                    return Result.Failure<Gathering>(DomainErrors.Gathering.NullMaxNumberOfAttendees);
                }

                gathering.MaximumNumberOfAttendees = maxNumberOfAttendees;
                break;
            case GatheringType.WithExpirationForInvitations:
                if (invitationsValidBeforeInHours is null)
                {
                    return Result.Failure<Gathering>(DomainErrors.Gathering.NullInvitationExpiryTime);
                }

                gathering.InvitationsExpireAtUtc =
                    gathering.ScheduledAtUtc.AddHours(-invitationsValidBeforeInHours.Value);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(GatheringType));
        }

        return gathering;
    }

    public Result<Invitation> SendInvitation(Member member)
    {
        // Validate
        if (Creator.Id == member.Id)
        {
            return Result.Failure<Invitation>(DomainErrors.Gathering.InvitingCreator);
        }

        if (ScheduledAtUtc < DateTime.UtcNow)
        {
            return Result.Failure<Invitation>(DomainErrors.Gathering.AlreadyPassed);
        }

        // Create invitation
        var invitation = new Invitation(Guid.NewGuid(), member, this);

        _invitations.Add(invitation);

        return invitation;
    }

    public Attendee? AcceptInvitation(Invitation invitation)
    {
        // Check if expired
        var expired = (Type == GatheringType.WithFixedNumberOfAttendees &&
                       NumberOfAttendees < MaximumNumberOfAttendees) ||
                      (Type == GatheringType.WithExpirationForInvitations &&
                       InvitationsExpireAtUtc < DateTime.UtcNow);

        if (expired)
        {
            invitation.Expire();
            return null;
        }

        var attendee = invitation.Accept(); ;

        _attendees.Add(attendee);
        NumberOfAttendees++;

        return attendee;
    }
}