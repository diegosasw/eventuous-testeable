using Eventuous;

namespace BasketballGame.Domain;

public static class GameEvents
{
	[EventType("V1.GameScheduled")]
	public record GameScheduled(
		string Id,
		string LocalTeamCode,
		string VisitorTeamCode,
		DateTime StartsOn);
	
	[EventType("V1.GameStarted")]
	public record GameStarted;
	
	[EventType("V1.GameEnded")]
	public record GameEnded;
	
	[EventType("V1.GameLocalTeamScored")]
	public record GameLocalTeamScored(string PlayerCode);
	
	[EventType("V1.GameVisitorTeamScored")]
	public record GameVisitorTeamScored(string PlayerCode);
}