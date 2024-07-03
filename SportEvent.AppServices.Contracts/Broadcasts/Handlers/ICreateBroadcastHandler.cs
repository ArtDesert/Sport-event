using SportEvent.Api.Contracts.Broadcasts.Requests;

namespace SportEvent.AppServices.Contracts.Broadcasts.Handlers;
public interface ICreateBroadcastHandler
{
	Task<long> HandleAsync(CreateBroadcastRequest broadcastRequest, CancellationToken token = default);
}
