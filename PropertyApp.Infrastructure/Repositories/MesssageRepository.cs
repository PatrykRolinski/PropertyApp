using Microsoft.EntityFrameworkCore;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Models;
using PropertyApp.Domain.Entities;
using System.Linq;

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
        && m.PropertyId==propertyId)|| (m.RecipientId==senderId && m.SenderId==reciepientId && m.PropertyId == propertyId))
            .OrderByDescending(m=> m.SendDate).ToListAsync();
        return messageThread;
    }
    public async Task <PaginationHelper<Message>> GetMessages(string container, Guid currentUserId, int PageSize, int PageNumber)
    {

        if (container == "Inbox")
        {

            var baseQuery =  _appContext.Messages.Where(x => x.RecipientId == currentUserId).AsQueryable();

            var totalItems = baseQuery.Count();
            baseQuery = baseQuery.OrderByDescending(x => x.SendDate);
            var messages =await baseQuery
                .Skip(PageSize * (PageNumber - 1))
                .Take(PageSize)
                .ToListAsync();

            var result = new PaginationHelper<Message> { Items = messages, totalCount = totalItems };
            return result;
        }
        else
        {

            var baseQuery =  _appContext.Messages.Where(x => x.SenderId == currentUserId).AsQueryable();
            var totalItems = baseQuery.Count();
            baseQuery = baseQuery.OrderByDescending(x => x.SendDate);
            var messages = await baseQuery
                .Skip(PageSize * (PageNumber - 1))
                .Take(PageSize)
                .ToListAsync();

            var result = new PaginationHelper<Message> { Items = messages, totalCount = totalItems };
            return result;
        }
       
    }
    public async Task SaveAllAsync()
    {
        await _appContext.SaveChangesAsync();
    }


}
