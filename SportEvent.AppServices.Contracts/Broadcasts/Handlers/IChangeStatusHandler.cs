using SportEvent.Api.Contracts.Broadcasts.Enums;

namespace SportEvent.AppServices.Contracts.Broadcasts.Handlers;

public interface IChangeStatusHandler
{
	Task HandleAsync(long broadcastId, BroadcastStatus newStatus, CancellationToken token);
}
