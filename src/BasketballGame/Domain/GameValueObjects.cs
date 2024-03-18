using Eventuous;

namespace BasketballGame.Domain;

public static class GameValueObjects
{
	public record TeamId(string Value)
		: Id(Value);
}