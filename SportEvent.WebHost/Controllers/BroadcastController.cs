using Microsoft.AspNetCore.Mvc;
using SportEvent.Api.Contracts.Abstractions;
using SportEvent.Api.Contracts.Broadcasts.Enums;
using SportEvent.Api.Contracts.Broadcasts.Requests;
using SportEvent.Api.Contracts.Broadcasts.Responses;
using SportEvent.AppServices.Contracts.Broadcasts.Handlers;

namespace SportEvent.WebHost.Controllers;

[ApiController]
[Route("broadcasts")]
public class BroadcastController : IBroadcastController
{
	private readonly ICreateBroadcastHandler _createBroadcastHandler;
	private readonly IChangeStatusHandler _changeStatusHandler;
	private readonly IGetBroadcastsByDateHandler _getBroadcastsByDateHandler;
	private readonly IGetBroadcastByIdHandler _getBroadcastByIdHandler;

	public BroadcastController(ICreateBroadcastHandler createBroadcastHandler,
							   IChangeStatusHandler changeStatusHandler,
							   IGetBroadcastsByDateHandler getBroadcastsByDateHandler,
							   IGetBroadcastByIdHandler getBroadcastByIdHandler)
	{
		_createBroadcastHandler = createBroadcastHandler ?? throw new ArgumentNullException(nameof(createBroadcastHandler));
		_changeStatusHandler = changeStatusHandler ?? throw new ArgumentNullException(nameof(changeStatusHandler));
		_getBroadcastsByDateHandler = getBroadcastsByDateHandler ?? throw new ArgumentNullException(nameof(getBroadcastsByDateHandler));
		_getBroadcastByIdHandler = getBroadcastByIdHandler ?? throw new ArgumentNullException(nameof(getBroadcastByIdHandler));
	}

	[HttpPost, Route("create")]
	public async Task<long> CreateBroadcastAsync([FromBody]CreateBroadcastRequest broadcast, CancellationToken token)
	{
		return await _createBroadcastHandler.HandleAsync(broadcast, token);
	}

	[HttpPost, Route("get-by-date")]
	public async Task<IEnumerable<BroadcastResponse>> GetBroadcastsByDateAsync(DateOnly date, CancellationToken token)
	{
		return await _getBroadcastsByDateHandler.HandleAsync(date, token);
	}

	[HttpPost, Route("change-status")]
	public async Task ChangeBroadcastStatus(long broadcastId, BroadcastStatus newStatus, CancellationToken token)
	{
		await _changeStatusHandler.HandleAsync(broadcastId, newStatus, token);
	}

	[HttpGet, Route("{broadcastId:long}")]
	public async Task<BroadcastResponse> GetBroadcastByIdAsync(long broadcastId, CancellationToken token)
	{
		return await _getBroadcastByIdHandler.HandleAsync(broadcastId, token);
	}
}
