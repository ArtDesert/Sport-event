using SportEvent.Api.Contracts.Broadcasts.Enums;
using SportEvent.AppServices.Contracts.BaseContracts;
using SportEvent.AppServices.Contracts.Broadcasts.Handlers;

namespace SportEvent.AppServices.Broadcasts.Handlers;

internal class ChangeStatusHandler : IChangeStatusHandler
{
	private readonly IBroadcastRepository _repository;

	public ChangeStatusHandler(IBroadcastRepository repository)
	{
		_repository = repository ?? throw new ArgumentNullException(nameof(repository));
	}

	public async Task HandleAsync(long broadcastId, BroadcastStatus newStatus, CancellationToken token)
	{
		var broadcast = await _repository.GetByIdAsync(broadcastId, token);
		broadcast.Status = newStatus;
		await _repository.UpdateAsync(broadcast, token);
	}
}
