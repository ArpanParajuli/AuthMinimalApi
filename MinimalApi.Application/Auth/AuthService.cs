using MinimalApi.Application.Abstractions;
using MinimalApi.Domain.Entities;
using MinimalApi.Domain.ValueObjects;

namespace MinimalApi.Application.Auth;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _jwtProvider = jwtProvider ?? throw new ArgumentNullException(nameof(jwtProvider));
    }

    public async Task<string> RegisterAsync(string rawEmail, string rawPassword, CancellationToken ct = default)
    {
        var email = Email.Create(rawEmail);

        // Fixed: Passing email.Value (string) to match your Repository interface
        if (!await _userRepository.IsEmailUniqueAsync(email.Value, ct))
            throw new InvalidOperationException("This email is already registered.");

        var passwordHash = _passwordHasher.Hash(rawPassword);
        var user = new User(Password.Create(passwordHash), email);

        await _userRepository.AddAsync(user, ct);
        return _jwtProvider.Generate(user);
    }

    public async Task<string> LoginAsync(string rawEmail, string rawPassword, CancellationToken ct = default)
    {
        var email = Email.Create(rawEmail);

        var user = await _userRepository.GetByEmailAsync(email.Value, ct);

        // Industry Practice: Use generic "Invalid credentials" for security
        if (user is null || !_passwordHasher.Verify(rawPassword, user.Password.Value))
            throw new UnauthorizedAccessException("Invalid email or password.");

        return _jwtProvider.Generate(user);
    }
}