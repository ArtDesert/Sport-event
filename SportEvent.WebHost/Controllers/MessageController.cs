using Microsoft.AspNetCore.Mvc;
using SportEvent.Api.Contracts.Abstractions;
using SportEvent.Api.Contracts.Messages.Requests;
using SportEvent.Api.Contracts.Messages.Responses;
using SportEvent.AppServices.Contracts.Messages.Handlers;

namespace SportEvent.WebHost.Controllers;

[ApiController]
[Route("messages")]
public class MessageController : IMessageController
{
	private readonly ICreateMessageHandler _createMessageHandler;

	public MessageController(ICreateMessageHandler createMessageHandler)
	{
		_createMessageHandler = createMessageHandler ?? throw new ArgumentNullException(nameof(createMessageHandler));
	}

	[HttpPost]
	public async Task<long> CreateMessageAsync([FromBody]CreateMessageRequest message, CancellationToken token)
	{
		return await _createMessageHandler.HandleAsync(message, token);
	}

	[HttpGet]
	public Task<ICollection<MessageResponse>> GetMessagesByBroadcastId(long broadcastId, CancellationToken token)
	{
		throw new NotImplementedException();
	}
}
