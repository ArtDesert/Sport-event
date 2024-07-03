using Microsoft.EntityFrameworkCore;
using SportEvent.AppServices.Contracts.BaseContracts;
using SportEvent.AppServices.Contracts.Entities;

namespace SportEvent.DataAccess.Repositories;

public class MessageRepository : IMessageRepository
{
	private readonly SportEventContext _context;

	public MessageRepository(SportEventContext context)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
	}

	public async Task<long> CreateAsync(Message entity, CancellationToken token = default)
	{
		await _context.Messages.AddAsync(entity, token);
		await _context.SaveChangesAsync(token);
		return entity.Id;
	}

	public async Task DeleteAsync(long id, CancellationToken token = default)
	{
		_context.Remove(new Message() { Id = id });
		await _context.SaveChangesAsync(token);
	}

	public async Task<Message?> GetByIdAsync(long id, CancellationToken token = default)
	{
		return await _context.Messages.FirstOrDefaultAsync(x => x.Id == id, token);
	}

	public async Task UpdateAsync(Message entity, CancellationToken token = default)
	{
		_context.Messages.Update(entity);
		await _context.SaveChangesAsync(token);
	}
}
