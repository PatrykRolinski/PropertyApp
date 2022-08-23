using FluentValidation;
using MediatR;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Contracts.IServices;


namespace PropertyApp.Application.Functions.Users.Commands.UpdateUser;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IUserRepository _userRepository;
   

    public UpdateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;       
    }

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateUserValidator(_userRepository);
        await validator.ValidateAndThrowAsync(request, cancellationToken);
        var user =await _userRepository.GetByIdAsync(request.UserId);
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        await _userRepository.UpdateAsync(user);
        return Unit.Value;
    }
}
