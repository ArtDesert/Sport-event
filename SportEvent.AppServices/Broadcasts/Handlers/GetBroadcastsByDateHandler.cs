using SportEvent.Api.Contracts.Broadcasts.Responses;
using SportEvent.AppServices.Contracts.BaseContracts;
using SportEvent.AppServices.Contracts.Broadcasts.Handlers;

namespace SportEvent.AppServices.Broadcasts.Handlers;

internal class GetBroadcastsByDateHandler : IGetBroadcastsByDateHandler
{
	private readonly IBroadcastRepository _repository;

	public GetBroadcastsByDateHandler(IBroadcastRepository repository)
	{
		_repository = repository ?? throw new ArgumentNullException(nameof(repository));
	}

	public async Task<IEnumerable<BroadcastResponse>> HandleAsync(DateOnly date, CancellationToken token)
	{
		return await _repository.GetBroadcastsByDateAsync(date, token);
	}
}
