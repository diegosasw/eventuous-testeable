using BasketballGame.Domain;
using FluentAssertions;
using TestCommon;

namespace BasketballGame.UnitTests;

public static class ScheduleGameTests
{
	public class Given_Command
		: AggregateTest<Game>
	{
		protected override IEnumerable<object> GivenEvents()
			=> [];

		protected override Task When(Game aggregate)
		{
			aggregate.Schedule(
				new GameId("foo"),
				new GameValueObjects.TeamId("one"),
				new GameValueObjects.TeamId("two"),
				new DateTime(2024, 1, 1));

			return Task.CompletedTask;
		}

		[Fact]
		public void It_Should_Produce_Game_Scheduled()
		{
			Then().Result.Changes.Should()
				.Contain(new GameEvents.GameScheduled("foo", "one", "two", new DateTime(2024, 1, 1)));
		}
	}
}