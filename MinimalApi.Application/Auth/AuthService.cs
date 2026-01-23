using MinimalApi.Application.Abstractions;
using MinimalApi.Domain.Entities;
using MinimalApi.Domain.ValueObjects;

using MinimalApi.Application.Dtos;

namespace MinimalApi.Application.Auth;

public class AuthService : IAuthService
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

    public async Task<AuthResponse> RegisterAsync(RegisterRequestDto request, CancellationToken ct = default)
    {
        var email = Email.Create(request.Email);

        if (!await _userRepository.IsEmailUniqueAsync(email.Value, ct))
            throw new InvalidOperationException("This email is already registered.");

        var passwordHash = _passwordHasher.Hash(request.Password);
        var user = new User(Password.Create(passwordHash), email);

        await _userRepository.AddAsync(user, ct);

        var token = _jwtProvider.Generate(user);

        return new AuthResponse(
            token,
            new UserResponse(user.Pid, user.Email.Value, user.CreatedAt)
        );
    }

    public async Task<AuthResponse> LoginAsync(LoginRequestDto request, CancellationToken ct = default)
    {
        var email = Email.Create(request.Email);
        var user = await _userRepository.GetByEmailAsync(email.Value, ct);

        if (user is null || !_passwordHasher.Verify(request.Password, user.Password.Value))
            throw new UnauthorizedAccessException("Invalid email or password.");

        var token = _jwtProvider.Generate(user);

        return new AuthResponse(
            token,
            new UserResponse(user.Pid, user.Email.Value, user.CreatedAt)
        );
    }
}