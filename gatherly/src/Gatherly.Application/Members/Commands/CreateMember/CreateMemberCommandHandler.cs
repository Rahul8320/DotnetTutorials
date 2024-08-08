using Gatherly.Domain.Entities;
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
        var memberResult = ValidateAndCreateMember(request);

        if (memberResult.IsFailure)
        {
            // Log error
            return Result.Failure<Unit>(memberResult.Errors);
        }

        _memberRepository.Add(memberResult.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private static Result<Member> ValidateAndCreateMember(CreateMemberCommand request)
    {
        List<Error> errors = new();

        var firstNameResult = FirstName.Create(request.FirstName);
        var lastNameResult = LastName.Create(request.LastName);

        if (firstNameResult.IsFailure || lastNameResult.IsFailure)
        {
            errors.Add(firstNameResult.Error);
            errors.Add(lastNameResult.Error);

            return Result.Failure<Member>(errors);
        }

        var member = new Member(
            Guid.NewGuid(),
            firstNameResult.Value,
            lastNameResult.Value,
            request.Email
        );

        return member;
    }
}
