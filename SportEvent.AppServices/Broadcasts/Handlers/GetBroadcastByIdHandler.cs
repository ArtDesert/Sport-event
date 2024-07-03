using SportEvent.Api.Contracts.Broadcasts.Responses;
using SportEvent.AppServices.Contracts.BaseContracts;
using SportEvent.AppServices.Contracts.Broadcasts.Handlers;

namespace SportEvent.AppServices.Broadcasts.Handlers;

internal class GetBroadcastByIdHandler : IGetBroadcastByIdHandler
{
	private readonly IBroadcastRepository _repository;

	public GetBroadcastByIdHandler(IBroadcastRepository repository)
	{
		_repository = repository ?? throw new ArgumentNullException(nameof(repository));
	}

	public async Task<BroadcastResponse> HandleAsync(long id, CancellationToken token)
	{
		var broadcast = await _repository.GetByIdAsync(id, token);
		return new BroadcastResponse()
		{
			Id = broadcast.Id,
			HomeTeam = broadcast.HomeTeam,
			GuestTeam = broadcast.GuestTeam,
			StartDate = broadcast.StartDate,
			StartTime = broadcast.StartTime,
			Status = broadcast.Status
		};
	}
}
