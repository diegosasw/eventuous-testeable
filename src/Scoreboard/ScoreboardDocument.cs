using Eventuous.Projections.MongoDB.Tools;

namespace Scoreboard;

public record ScoreboardDocument
	: ProjectedDocument
{
	public ScoreboardDocument(string Id) 
		: base(Id)
	{
	}
}