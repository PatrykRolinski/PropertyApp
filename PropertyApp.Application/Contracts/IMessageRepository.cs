

using PropertyApp.Domain.Entities;

namespace PropertyApp.Application.Contracts;

public interface IMessageRepository
{
    public Task AddAsync(Message entity);
    public Task<List<Message>> GetMessageThread(Guid reciepientId, Guid senderId, int propertyId);
    public Task<IReadOnlyList<Message>> GetMessages(string container, Guid currentUserId);
    public Task SaveAllAsync();
}
