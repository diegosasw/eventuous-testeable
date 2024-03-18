using Eventuous;
using static BasketballGame.Domain.GameValueObjects;

namespace BasketballGame.Domain;

public class Game
	: Aggregate<GameState>
{
	public void Schedule(GameId gameId, TeamId localTeamId, TeamId visitorTeamId, DateTime startsOn)
	{
		EnsureDoesntExist();
		var gameScheduled = new GameEvents.GameScheduled(gameId, localTeamId, visitorTeamId, startsOn);
		Apply(gameScheduled);
	}
}