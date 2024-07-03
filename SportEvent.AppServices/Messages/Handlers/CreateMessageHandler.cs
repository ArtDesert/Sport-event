using SportEvent.Api.Contracts.Messages.Requests;
using SportEvent.AppServices.Contracts.BaseContracts;
using SportEvent.AppServices.Contracts.Entities;
using SportEvent.AppServices.Contracts.Messages.Handlers;

namespace SportEvent.AppServices.Messages.Handlers;

internal class CreateMessageHandler : ICreateMessageHandler
{
	private readonly IMessageRepository _repository;

	public CreateMessageHandler(IMessageRepository repository)
	{
		_repository = repository ?? throw new ArgumentNullException(nameof(repository));
	}

	public async Task<long> HandleAsync(CreateMessageRequest messageRequest, CancellationToken token = default)
	{
		var message = new Message()
		{
			Time = messageRequest.Time,
			Action = messageRequest.Action,
			Value = messageRequest.Value,
			Subject = messageRequest.Subject,
			Info = messageRequest.Info,
			BroadcastId = messageRequest.BroadcastId
		};
		return await _repository.CreateAsync(message, token);
	}
}
