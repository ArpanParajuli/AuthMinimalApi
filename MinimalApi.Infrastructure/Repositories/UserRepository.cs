using Microsoft.EntityFrameworkCore;
using MinimalApi.Application.Abstractions;
using MinimalApi.Domain.Entities;
using MinimalApi.Infrastructure.Persistence; // Your DbContext namespace

namespace MinimalApi.Infrastructure.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly AuthDbContext _context;

    public UserRepository(AuthDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid pid, CancellationToken ct = default)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Pid == pid, ct);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
    {
        // We compare the Value property of the Email Value Object
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email.Value == email, ct);
    }

    public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken ct = default)
    {
        return !await _context.Users
            .AnyAsync(u => u.Email.Value == email, ct);
    }

    public async Task AddAsync(User user, CancellationToken ct = default)
    {
        await _context.Users.AddAsync(user, ct);
        // Note: In many clean architecture setups, SaveChangesAsync
        // is called by a Unit of Work or the Service, not the Repository.
        await _context.SaveChangesAsync(ct);
    }

    public void Update(User user)
    {
        // EF Core tracks changes to entities fetched from the context,
        // so often you just need to update the UpdatedAt property.
        _context.Users.Update(user);
    }
}