using Gatherly.Application.Abstractions;
using Gatherly.Domain.Enums;
using Gatherly.Domain.Repositories;
using MediatR;

namespace Gatherly.Application.Invitations.Commands.AcceptInvitation;

internal sealed class AcceptInvitationCommandHandler(
    IGatheringRepository gatheringRepository,
    IAttendeeRepository attendeeRepository,
    IUnitOfWork unitOfWork,
    IEmailService emailService) : IRequestHandler<AcceptInvitationCommand>
{
    public async Task<Unit> Handle(AcceptInvitationCommand request, CancellationToken cancellationToken)
    {
        var gathering = await gatheringRepository
            .GetByIdWithCreatorAsync(request.GatheringId, cancellationToken);

        if (gathering is null)
        {
            return Unit.Value;
        }

        var invitation = gathering.Invitations.FirstOrDefault(i => i.Id == request.InvitationId);

        if (invitation is null || invitation.Status != InvitationStatus.Pending)
        {
            return Unit.Value;
        }

        var attendee = gathering.AcceptInvitation(invitation);

        if (attendee is not null)
        {
            attendeeRepository.Add(attendee);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        // Send email
        if (invitation.Status == InvitationStatus.Accepted)
        {
            await emailService.SendInvitationAcceptedEmailAsync(gathering, cancellationToken);
        }

        return Unit.Value;
    }
}