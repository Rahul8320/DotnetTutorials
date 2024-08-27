using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Errors;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;
using Gatherly.Domain.ValueObjects;
using MediatR;

namespace Gatherly.Application.Members.Commands.CreateMember;

public class CreateMemberCommandHandler(
    IMemberRepository memberRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateMemberCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        var memberResult = await ValidateAndCreateMember(request, cancellationToken);

        if (memberResult.IsFailure)
        {
            // Log error
            return Result.Failure<Guid>(memberResult.Errors);
        }

        memberRepository.Add(memberResult.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(memberResult.Value.Id);
    }

    private async Task<Result<Member>> ValidateAndCreateMember(CreateMemberCommand request, CancellationToken cancelToken)
    {
        List<Error> errors = [];

        var firstNameResult = FirstName.Create(request.FirstName);
        var lastNameResult = LastName.Create(request.LastName);
        var emailResult = Email.Create(request.Email);

        if (firstNameResult.IsFailure || lastNameResult.IsFailure || emailResult.IsFailure)
        {
            errors.Add(firstNameResult.Error);
            errors.Add(lastNameResult.Error);
            errors.Add(emailResult.Error);

            return Result.Failure<Member>(errors);
        }

        var isUniqueEmail = await memberRepository.IsEmailUnique(emailResult.Value, cancelToken);

        if (isUniqueEmail == false)
        {
            errors.Add(ValidationErrors.Email.Register);

            return Result.Failure<Member>(errors);
        }

        var member = Member.Create(
            Guid.NewGuid(),
            firstNameResult.Value,
            lastNameResult.Value,
            emailResult.Value
        );

        return member;
    }
}
