using Microsoft.EntityFrameworkCore;
using PropertyApp.Application.Contracts;
using PropertyApp.Domain.Entities;

namespace PropertyApp.Infrastructure.Repositories;

public class MesssageRepository : IMessageRepository
{
    private readonly PropertyAppContext _appContext;

    public MesssageRepository(PropertyAppContext appContext)
    {
        _appContext = appContext;
    }
    public async Task AddAsync(Message entity)
    {
      await  _appContext.AddAsync(entity);
      await  _appContext.SaveChangesAsync();
    }
    public async Task<List<Message>> GetMessageThread(Guid reciepientId, Guid senderId, int propertyId)
    {
      var messageThread= await _appContext.Messages.Where(m=> (m.RecipientId==reciepientId && m.SenderId==senderId 
        && m.PropertyId==propertyId)|| (m.RecipientId==senderId && m.SenderId==reciepientId && m.PropertyId == propertyId)).ToListAsync();
        return messageThread;
    }
    public async Task <IReadOnlyList<Message>> GetMessages(string container, Guid currentUserId)
    {

        if (container == "Inbox")
        {

          var messages=await  _appContext.Messages.Where(x => x.RecipientId == currentUserId).ToListAsync();
            return messages;
        }
        else
        {
            var messages = await _appContext.Messages.Where(x => x.SenderId == currentUserId).ToListAsync();
            return messages;
        }
       
    }
    public async Task SaveAllAsync()
    {
        await _appContext.SaveChangesAsync();
    }


}
