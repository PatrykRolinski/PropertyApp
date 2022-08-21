

using PropertyApp.Application.Models;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Application.Contracts;

public interface IMessageRepository
{
    public Task AddAsync(Message entity);
    public Task<List<Message>> GetMessageThread(Guid reciepientId, Guid senderId, int propertyId);
    public Task<MessagePagination> GetMessages(string container, Guid currentUserId, int PageSize, int PageNumber);
    public Task SaveAllAsync();
}
