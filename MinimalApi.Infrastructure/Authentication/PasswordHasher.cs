using BCrypt.Net;
using MinimalApi.Application.Abstractions;

namespace MinimalApi.Infrastructure.Authentication;

internal sealed class PasswordHasher : IPasswordHasher
{
    // A work factor of 12 is recommended for modern systems
    private const int WorkFactor = 12;

    public string Hash(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty.", nameof(password));

        // BCrypt handles generating a unique salt and prepending it to the hash automatically
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password, WorkFactor);
    }

    public bool Verify(string password, string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(passwordHash))
            return false;

        // EnhancedVerify supports longer passwords and is more secure than the legacy Verify
        return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
    }
}