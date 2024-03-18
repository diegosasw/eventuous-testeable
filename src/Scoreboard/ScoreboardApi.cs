using Microsoft.AspNetCore.Mvc;

namespace Scoreboard;

public class ScoreboardApi
	: ControllerBase
{
	private readonly QueryService<ScoreboardDocument> _scoreboardQueryService;

	public ScoreboardApi(QueryService<ScoreboardDocument> scoreboardQueryService)
		=> _scoreboardQueryService = scoreboardQueryService;
	
	[HttpGet]
	public async Task<IEnumerable<ScoreboardDocument>> GetAll(
		CancellationToken cancellationToken)
	{
		var documents = 
			await _scoreboardQueryService.Get(x => !string.IsNullOrWhiteSpace(x.Id), cancellationToken);
		return documents;
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<ScoreboardDocument>> Get(string id, CancellationToken cancellationToken)
	{
		var document = await _scoreboardQueryService.GetSingleOrDefault(id, cancellationToken);
		return Ok(document);
	}
}