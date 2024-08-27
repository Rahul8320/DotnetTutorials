using Gatherly.Application.Abstractions;
using Gatherly.Domain.Entities;

namespace Gatherly.Infrastucture.Services;

internal sealed class EmailService : IEmailService
{
    public Task SendInvitationAcceptedEmailAsync(
        Gathering gathering,
        CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task SendInvitationSentEmailAsync(
        Member member,
        Gathering gathering,
        CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
