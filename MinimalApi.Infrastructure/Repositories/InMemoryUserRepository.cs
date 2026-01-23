using System.Collections.Concurrent;
using MinimalApi.Application.Abstractions;
using MinimalApi.Domain.Entities;

namespace MinimalApi.Infrastructure.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    // A thread-safe dictionary to act as our "Database"
    private static readonly ConcurrentDictionary<Guid, User> _users = new();

    public Task<User?> GetByIdAsync(Guid pid, CancellationToken ct = default)
    {
        var user = _users.Values.FirstOrDefault(u => u.Pid == pid);
        return Task.FromResult(user);
    }

    public Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
    {
        // Remember to compare the .Value of the Email Value Object
        var user = _users.Values.FirstOrDefault(u => u.Email.Value == email);
        return Task.FromResult(user);
    }

    public Task<bool> IsEmailUniqueAsync(string email, CancellationToken ct = default)
    {
        bool exists = _users.Values.Any(u => u.Email.Value == email);
        return Task.FromResult(!exists);
    }

    public Task AddAsync(User user, CancellationToken ct = default)
    {
        // We use Pid as the key for in-memory storage
        _users.TryAdd(user.Pid, user);
        return Task.CompletedTask;
    }

    public void Update(User user)
    {
        if (_users.ContainsKey(user.Pid))
        {
            _users[user.Pid] = user;
        }
    }
}