using BasketballGame.Domain;
using Eventuous;
using static BasketballGame.Domain.GameCommands;

namespace BasketballGame.Application;

public class GameCommandService
	: CommandService<Game, GameState, GameId>
{
	public GameCommandService(
		IAggregateStore store) 
		: base(store)
	{
		On<ScheduleGame>()
			.InState(ExpectedState.New)
			.GetId(cmd => cmd.GameId)
			.Act((game, cmd) => game.Schedule(cmd.GameId, cmd.LocalTeamId, cmd.VisitorTeamId, cmd.StartsOn));
	}
}