using MediatR;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Exceptions;

namespace PropertyApp.Application.Functions.Users.Commands.VerifyUser;

public class VerifyUserHandler : IRequestHandler<VerifyUserCommand>
{
    private readonly IUserRepository _userRepository;

    public VerifyUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(VerifyUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindyByVerificationToken(request.Token);
        if (user == null || user.VerifiedAt!=null)
        {
            throw new NotVerifiedException("Verification failed");
        }

        user.VerifiedAt = DateTime.UtcNow;
        await _userRepository.UpdateAsync(user);

        return Unit.Value;
    }
}
