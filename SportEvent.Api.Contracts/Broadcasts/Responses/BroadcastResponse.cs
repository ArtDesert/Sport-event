using SportEvent.Api.Contracts.Broadcasts.Enums;

namespace SportEvent.Api.Contracts.Broadcasts.Responses;

public class BroadcastResponse
{
    public long Id { get; set; }
	public string HomeTeam { get; set; }
	public string GuestTeam { get; set; }
	public DateOnly StartDate { get; set; }
	public TimeOnly StartTime { get; set; }
	public BroadcastStatus Status { get; set; }

	public override string ToString()
	{
		string rusStatus;
		int? minutes = null, time = 1;
		switch (Status)
		{
			case BroadcastStatus.NotStarted:
				rusStatus = "трансляция ещё не началась";
				break;
			case BroadcastStatus.InProgress:
				rusStatus = "трансляция идёт";
				minutes = int.Parse(DateTime.Now.ToShortTimeString().Split(":")[1]) - int.Parse(StartTime.ToShortTimeString().Split(":")[1]);
				if (minutes > 45)
				{
					++time;
				}
				break;
			case BroadcastStatus.Completed:
				rusStatus = "трансляция завершена";
				break;
			default:
				rusStatus = "неизвестная трансляция";
				break;
		}
		string result = $"{Id}. {HomeTeam} - {GuestTeam}, {StartTime}, {rusStatus}";
		if (Status == BroadcastStatus.InProgress)
		{
			result = $"{result}, {minutes} минута, {time} тайм";
		}
		return result;
	}
}
