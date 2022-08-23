using AutoMapper;
using MediatR;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Application.Exceptions;
using PropertyApp.Application.Models;

namespace PropertyApp.Application.Functions.Messages.Queries.GetMessageThread
{
    public class GetMessageThreadHandler : IRequestHandler<GetMessageThreadQuery, GetMessageThreadDto>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetMessageThreadHandler(IMessageRepository messageRepository, ICurrentUserService currentUserService, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<GetMessageThreadDto> Handle(GetMessageThreadQuery request, CancellationToken cancellationToken)
        {
           
           var currentUserId = _currentUserService.UserId;

            if (currentUserId == null)
            {
                throw new NotFoundException($"User with id {currentUserId} is not found");
            }
            var messageThread= await _messageRepository.GetMessageThread(Guid.Parse(currentUserId), request.SenderId, request.PropertyId);

            foreach(var message in messageThread)
            {
                if(message.DateRead==null && message.RecipientId == Guid.Parse(currentUserId))
                {
                    message.DateRead=DateTime.Now;

                }
               await _messageRepository.SaveAllAsync();
            }

            var messagesDto = _mapper.Map<List<MessageDto>>(messageThread);
            var messageThreadDto = new GetMessageThreadDto()
            {
                PropertyId = request.PropertyId,
                Messages = messagesDto
            };
            return messageThreadDto;
           
        }
    }
}