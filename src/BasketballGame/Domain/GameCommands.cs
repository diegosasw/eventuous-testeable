using static BasketballGame.Domain.GameValueObjects;

namespace BasketballGame.Domain;

public static class GameCommands
{
	public record ScheduleGame(
		GameId GameId,
		TeamId LocalTeamId, 
		TeamId VisitorTeamId, 
		DateTime StartsOn);
}