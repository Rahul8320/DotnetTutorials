using Gatherly.Domain.Enums;
using Gatherly.Domain.primitives;

namespace Gatherly.Domain.Entities;

public sealed class Gathering : Entity
{
    private readonly List<Invitation> _invitations = new();
    private readonly List<Attendee> _attendees = new();

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

    public static Gathering Create(
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
                    throw new Exception(
                        $"{nameof(maxNumberOfAttendees)} can't be null.");
                }

                gathering.MaximumNumberOfAttendees = maxNumberOfAttendees;
                break;
            case GatheringType.WithExpirationForInvitations:
                if (invitationsValidBeforeInHours is null)
                {
                    throw new Exception(
                        $"{nameof(invitationsValidBeforeInHours)} can't be null.");
                }

                gathering.InvitationsExpireAtUtc =
                    gathering.ScheduledAtUtc.AddHours(-invitationsValidBeforeInHours.Value);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(GatheringType));
        }

        return gathering;
    }

    public Invitation SendInvitation(Member member)
    {
        // Validate
        if (Creator.Id == member.Id)
        {
            throw new Exception("Can't send invitation to the gathering creator.");
        }

        if (ScheduledAtUtc < DateTime.UtcNow)
        {
            throw new Exception("Can't send invitation for gathering in the past.");
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