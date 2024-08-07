using Gatherly.Domain.Shared;

namespace Gatherly.Domain.Errors;

public static class DomainErrors
{
    public static class Gathering
    {
        public static readonly Error InvitingCreator = new(
            "Gathering.InvitingCreator",
            "Can't send invitation to the gathering creator.");

        public static readonly Error AlreadyPassed = new(
            "Gathering.AlreadyPassed",
            "Can't send invitation for gathering in the past.");

        public static readonly Error NullMaxNumberOfAttendees = new(
            "Gathering.NullMaxNumberOfAttendees",
            "Max number of attendees can't be null.");

        public static readonly Error NullInvitationExpiryTime = new(
            "Gathering.NullInvitationExpiryTime",
            "Invitation expire time can't be null.");
    }
}
