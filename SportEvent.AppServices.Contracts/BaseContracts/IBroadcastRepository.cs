using SportEvent.Api.Contracts.Broadcasts.Responses;
using SportEvent.AppServices.Contracts.Entities;

namespace SportEvent.AppServices.Contracts.BaseContracts;

public interface IBroadcastRepository : IBaseRepository<Broadcast, long>
{
	Task<IEnumerable<BroadcastResponse>> GetBroadcastsByDateAsync(DateOnly date, CancellationToken token);
}
