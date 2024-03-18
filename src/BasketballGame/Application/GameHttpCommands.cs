namespace BasketballGame.Application;

public static class GameHttpCommands
{
	public record ScheduleGameHttp(
		string LocalTeamId,
		string VisitorTeamId,
		DateTime StartsOn);
}