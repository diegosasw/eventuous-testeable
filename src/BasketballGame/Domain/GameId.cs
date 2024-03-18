using Eventuous;

namespace BasketballGame.Domain;

public record GameId(string Value)
	: Id(Value)
{
}