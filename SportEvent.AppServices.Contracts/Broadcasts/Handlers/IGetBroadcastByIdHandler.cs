using SportEvent.Api.Contracts.Broadcasts.Responses;

namespace SportEvent.AppServices.Contracts.Broadcasts.Handlers;

public interface IGetBroadcastByIdHandler
{
	Task<BroadcastResponse> HandleAsync(long id, CancellationToken token);
}
