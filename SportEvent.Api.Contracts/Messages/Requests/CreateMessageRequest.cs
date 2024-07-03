namespace SportEvent.Api.Contracts.Messages.Requests;
public class CreateMessageRequest
{
    public CreateMessageRequest(long broadcastId, string message)
    {
		BroadcastId = broadcastId;
		var tokens = message.Split(';');
		int length = tokens.Length;
		Action = tokens[0];
		Info = tokens[length - 1];
		Subject = tokens[length - 2];
		if (length == 4)
		{
			Value = tokens[1];
		}
		Time = new TimeOnly(DateTime.Now.Hour, DateTime.Now.Minute);
	}
    public long BroadcastId { get; set; }
	public TimeOnly Time { get; set; }
	public string? Action { get; set; }
	public string? Value { get; set; }
	public string? Subject { get; set; }
	public string Info { get; set; }

}
