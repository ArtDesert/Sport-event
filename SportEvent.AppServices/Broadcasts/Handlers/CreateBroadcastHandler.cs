using SportEvent.Api.Contracts.Broadcasts.Enums;
using SportEvent.Api.Contracts.Broadcasts.Requests;
using SportEvent.AppServices.Contracts.BaseContracts;
using SportEvent.AppServices.Contracts.Broadcasts.Handlers;
using SportEvent.AppServices.Contracts.Entities;

namespace SportEvent.AppServices.Broadcasts.Handlers;

internal class CreateBroadcastHandler : ICreateBroadcastHandler
{
	private readonly IBroadcastRepository _repository;

	public CreateBroadcastHandler(IBroadcastRepository repository)
	{
		_repository = repository ?? throw new ArgumentNullException(nameof(repository));
	}

	public async Task<long> HandleAsync(CreateBroadcastRequest broadcastRequest, CancellationToken token = default)
	{
		var broadcast = new Broadcast()
		{
			HomeTeam = broadcastRequest.HomeTeam,
			GuestTeam = broadcastRequest.GuestTeam,
			StartDate = broadcastRequest.StartDate,
			StartTime = broadcastRequest.StartTime,
			Status = BroadcastStatus.NotStarted
		};
		return await _repository.CreateAsync(broadcast, token);
	}
}
