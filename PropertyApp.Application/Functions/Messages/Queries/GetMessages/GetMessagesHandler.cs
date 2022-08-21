using AutoMapper;
using MediatR;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Contracts.IServices;
using PropertyApp.Application.Models;

namespace PropertyApp.Application.Functions.Messages.Queries.GetMessages
{
    public class GetMessagesHandler : IRequestHandler<GetMessagesQuery, PageResult<MessageDto>>
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

        public async Task<PageResult<MessageDto>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
         var messagePaginationDto= await _messageRepository.GetMessages(request.Container, Guid.Parse(userId), request.PageSize, request.PageNumber);
          var messagesDto=_mapper.Map<List<MessageDto>>(messagePaginationDto.Messages);


          var result = new PageResult<MessageDto>(messagesDto, request.PageNumber, messagePaginationDto.totalCount, request.PageSize);
            return result;
        }
    }
}