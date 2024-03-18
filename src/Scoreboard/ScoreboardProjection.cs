using Eventuous.Projections.MongoDB;
using Eventuous.Subscriptions.Context;
using MongoDB.Driver;
using static BasketballGame.Domain.GameEvents;

namespace Scoreboard;

public class ScoreboardProjection
	: MongoProjector<ScoreboardDocument>
{
	public ScoreboardProjection(IMongoDatabase database) 
		: base(database)
	{
		On<GameScheduled>(Handler);
	}
	
	private static ValueTask<MongoProjectOperation<ScoreboardDocument>> Handler(
		IMessageConsumeContext<GameScheduled> ctx)
	{
		var gameScheduled = ctx.Message;
		var gameId = gameScheduled.Id;
		
		var filter = Builders<ScoreboardDocument>.Filter.Eq(x => x.Id, gameId);
		var document =
			new ScoreboardDocument(gameId)
			{
			};
		
		var replaceOptions = new ReplaceOptions {IsUpsert = true};

		var operation =
			new MongoProjectOperation<ScoreboardDocument>(async (collection, cancellationToken)
				=> await collection.ReplaceOneAsync(filter, document, replaceOptions, cancellationToken));

		return ValueTask.FromResult(operation);
	}
}