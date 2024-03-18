using Eventuous.Projections.MongoDB.Tools;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Scoreboard;

public class QueryService<TDocument>
	where TDocument : ProjectedDocument
{
	private readonly IMongoCollection<TDocument> _collection;

	public QueryService(IMongoDatabase database)
		=> _collection = database.GetDocumentCollection<TDocument>();
	
	public async Task<TDocument?> GetSingleOrDefault(
		string resourceId,
		CancellationToken cancellationToken = new())
	{
		var documents = await Get(x => x.Id == resourceId, cancellationToken);
		return documents.FirstOrDefault();
	}

	public async Task<IEnumerable<TDocument>> Get(
		Expression<Func<TDocument, bool>> filter,
		CancellationToken cancellationToken = new())
	{
		var queryResult = await _collection.FindAsync(filter, cancellationToken: cancellationToken);
		var documents = await queryResult.ToListAsync(cancellationToken);
		return documents;
	}
}