namespace SportEvent.Api.Contracts.Broadcasts.Requests;
public class CreateBroadcastRequest
{
    public string HomeTeam { get; set; }
    public string GuestTeam { get; set; }
    public DateOnly StartDate { get; set; }
    public TimeOnly StartTime { get; set; }
}
