using Microsoft.EntityFrameworkCore;
using SportEvent.Api.Contracts.Broadcasts.Responses;
using SportEvent.AppServices.Contracts.BaseContracts;
using SportEvent.AppServices.Contracts.Entities;

namespace SportEvent.DataAccess.Repositories;
public class BroadcastRepository : IBroadcastRepository
{
	private readonly SportEventContext _context;

    public BroadcastRepository(SportEventContext context)
    {
		_context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<long> CreateAsync(Broadcast entity, CancellationToken token = default)
	{
		await _context.Broadcasts.AddAsync(entity, token);
		await _context.SaveChangesAsync(token);
		return entity.Id;
	}

	public async Task DeleteAsync(long id, CancellationToken token = default)
	{
		_context.Broadcasts.Remove(new Broadcast() { Id = id });
		await _context.SaveChangesAsync(token);
	}

	public async Task<Broadcast?> GetByIdAsync(long id, CancellationToken token = default)
	{
		return await _context.Broadcasts.FirstOrDefaultAsync(x => x.Id == id, token);
	}

	public async Task UpdateAsync(Broadcast entity, CancellationToken token = default)
	{
		_context.Broadcasts.Update(entity);
		await _context.SaveChangesAsync(token);
	}

	public async Task<IEnumerable<BroadcastResponse>> GetBroadcastsByDateAsync(DateOnly date, CancellationToken token = default)
	{
		return await _context.Broadcasts
			.Where(broadcast => broadcast.StartDate == date)
			.Select(broadcast => new BroadcastResponse()
			{
				Id = broadcast.Id,
				HomeTeam = broadcast.HomeTeam,
				GuestTeam = broadcast.GuestTeam,
				StartDate = broadcast.StartDate,
				StartTime = broadcast.StartTime,
				Status = broadcast.Status
			})
			.ToListAsync(token);
	}
}
