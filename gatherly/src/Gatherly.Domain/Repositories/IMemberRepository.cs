﻿using Gatherly.Domain.Entities;
using Gatherly.Domain.ValueObjects;

namespace Gatherly.Domain.Repositories;

public interface IMemberRepository
{
    Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(Member member);
    Task<bool> IsEmailUnique(Email email, CancellationToken cancellationToken = default);
}