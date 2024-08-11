using Gatherly.Domain.Entities;
using Gatherly.Domain.Errors;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;
using Gatherly.Domain.ValueObjects;
using MediatR;

namespace Gatherly.Application.Members.Commands.CreateMember;

public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, Result<Unit>>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMemberCommandHandler(
        IMemberRepository memberRepository,
        IUnitOfWork unitOfWork)
    {
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit>> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        var memberResult = await ValidateAndCreateMember(request, cancellationToken);

        if (memberResult.IsFailure)
        {
            // Log error
            return Result.Failure<Unit>(memberResult.Errors);
        }

        _memberRepository.Add(memberResult.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private async Task<Result<Member>> ValidateAndCreateMember(CreateMemberCommand request, CancellationToken cancelToken)
    {
        List<Error> errors = new();

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

        var isUniqueEmail = await _memberRepository.IsEmailUnique(emailResult.Value, cancelToken);

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
