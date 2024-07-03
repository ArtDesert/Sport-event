using SportEvent.Api.Contracts.Broadcasts.Responses;

namespace SportEvent.AppServices.Contracts.Broadcasts.Handlers;

public interface IGetBroadcastsByDateHandler
{
	Task<IEnumerable<BroadcastResponse>> HandleAsync(DateOnly date, CancellationToken token);
}
