using BasketballGame.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using static BasketballGame.Application.GameHttpCommands;
using static BasketballGame.Domain.GameCommands;
using static BasketballGame.Domain.GameValueObjects;

namespace BasketballGame.Application;

public static class GameCommandsApi
{
	public static WebApplication AddClientCommands(this WebApplication app)
	{
		app
			.MapAggregateCommands<Game>()
			.MapCommand<ScheduleGameHttp, ScheduleGame>(
				"games/schedule",
				(cmd, ctx) =>
				{
					var domainCommand =
						new ScheduleGame(
							new GameId(Guid.NewGuid().ToString()),
							new TeamId(cmd.LocalTeamId),
							new TeamId(cmd.VisitorTeamId),
							cmd.StartsOn);
					return domainCommand;
				});

		return app;
	}
}