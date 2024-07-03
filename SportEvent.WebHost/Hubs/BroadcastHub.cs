using Microsoft.AspNetCore.SignalR;
using SportEvent.Api.Contracts.Abstractions;
using SportEvent.Api.Contracts.Broadcasts.Enums;
using SportEvent.Api.Contracts.Broadcasts.Requests;
using SportEvent.Api.Contracts.Broadcasts.Responses;
using SportEvent.Api.Contracts.Messages.Requests;

namespace SportEvent.WebHost.Hubs;

public interface IBroadcastClient
{ 
	Task Send(string message);
	Task SendBroadcasts(IEnumerable<BroadcastResponse> broadcasts);
	Task GetResultForFan(bool isCorrectBroadcastId, TimeOnly broadcastStartTime);
	Task GetResultForCommentator(bool isCorrectBroadcastId);
}

public class BroadcastHub : Hub<IBroadcastClient>
{
	private readonly IBroadcastController _broadcastController;
	private readonly IMessageController _messageController;

	public BroadcastHub(IBroadcastController broadcastController, IMessageController messageController)
	{
		_broadcastController = broadcastController ?? throw new ArgumentNullException(nameof(broadcastController));
		_messageController = messageController ?? throw new ArgumentNullException(nameof(messageController));
	}

	public async Task SendMessageToGroup(string groupName, string message)
	{
        await Clients.Group(groupName).Send(message);
	}

	public async Task GetBroadcastsByDate(DateOnly date)
	{
		var broadcasts = await _broadcastController.GetBroadcastsByDateAsync(date, CancellationToken.None);
		await Clients.Caller.SendBroadcasts(broadcasts);
	}

	public async Task RegisterBroadcast(CreateBroadcastRequest broadcastRequest)
	{
		await _broadcastController.CreateBroadcastAsync(broadcastRequest, CancellationToken.None);
	}

	public async Task JoinGroup(string groupName)
	{
		await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
	}

	public async Task ExitGroup(string groupName)
	{
		await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
	}

	public async Task AddMessageIntoDb(long broadcastId, string message)
	{
		var messageRequest = new CreateMessageRequest(broadcastId, message);
		await _messageController.CreateMessageAsync(messageRequest, CancellationToken.None);
	}

	public async Task ChangeStatus(long broadcastId, BroadcastStatus newStatus)
	{
		await _broadcastController.ChangeBroadcastStatus(broadcastId, newStatus, CancellationToken.None);
	}

	public async Task CheckBroadcastStatusForFan(long broadcastId)
	{
		var broadcast = await _broadcastController.GetBroadcastByIdAsync(broadcastId, CancellationToken.None);
		bool isCorrectBroadcastId;
		if (broadcast == null)
		{
			isCorrectBroadcastId = false;
		}
		else
		{
			isCorrectBroadcastId = broadcast.Status == BroadcastStatus.InProgress;
		}
		await Clients.Caller.GetResultForFan(isCorrectBroadcastId, broadcast.StartTime);
	}

	public async Task CheckBroadcastStatusForCommentator(long broadcastId)
	{
		var broadcast = await _broadcastController.GetBroadcastByIdAsync(broadcastId, CancellationToken.None);
		bool isCorrectBroadcastId;
		if (broadcast == null)
		{
			isCorrectBroadcastId = false;
		}
		else
		{
			isCorrectBroadcastId = broadcast.Status == BroadcastStatus.NotStarted;
		}
		await Clients.Caller.GetResultForCommentator(isCorrectBroadcastId);
	}

	public override async Task OnConnectedAsync()
	{
		await base.OnConnectedAsync();
	}
}
