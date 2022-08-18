using MediatR;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Application.Functions.Messages.Commands.AddMessage;

public class AddMessageHandler : IRequestHandler<AddMessageCommand, int>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public AddMessageHandler(IMessageRepository messageRepository, IUserRepository userRepository, ICurrentUserService currentUserService)
    {
        _messageRepository = messageRepository;
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task<int> Handle(AddMessageCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;
        var currentUser =await _userRepository.GetByIdAsync(Guid.Parse(currentUserId));
        var recipientUser= await _userRepository.GetByIdAsync(request.RecipientId);

        var message = new Message()
        {
            PropertyId = request.PropertyId,
            Content = request.Content,
            RecipientId = request.RecipientId,
            SenderId = Guid.Parse(currentUserId),
            SendDate = DateTime.UtcNow,
            SenderFirstName = currentUser.FirstName,
            ReciepientFirstName = recipientUser.FirstName
        };
        await _messageRepository.AddAsync(message);

        return message.Id;
    }
}
