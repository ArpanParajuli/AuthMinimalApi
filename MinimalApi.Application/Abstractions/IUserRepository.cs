using MinimalApi.Domain.Entities;

namespace MinimalApi.Application.Abstractions;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid pid, CancellationToken ct = default);

    Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);

    Task<bool> IsEmailUniqueAsync(string email, CancellationToken ct = default);

    Task AddAsync(User user, CancellationToken ct = default);

    void Update(User user);
}