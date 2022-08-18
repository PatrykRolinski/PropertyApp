using AutoMapper;
using MediatR;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Application.Models;

namespace PropertyApp.Application.Functions.Messages.Queries.GetMessages
{
    public class GetMessagesHandler : IRequestHandler<GetMessagesQuery, List<MessageDto>>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetMessagesHandler(IMessageRepository messageRepository, 
            ICurrentUserService currentUserService, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<List<MessageDto>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
         var messages= await _messageRepository.GetMessages(request.Container, Guid.Parse(userId));
          var messagesDto=_mapper.Map<List<MessageDto>>(messages);
            return messagesDto;
        }
    }
}