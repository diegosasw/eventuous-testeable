using Eventuous;
using Xunit;

namespace TestCommon;

public abstract class AggregateTest<TAggregate>
	: IAsyncLifetime
	where TAggregate : Aggregate
{
	private readonly AggregateFactoryRegistry _registry = AggregateFactoryRegistry.Instance;

	public Task InitializeAsync()
	{
		return Task.CompletedTask;
	}
	
	public async Task DisposeAsync() => await Cleanup();
	
	protected abstract IEnumerable<object> GivenEvents();
	
	protected abstract Task When(TAggregate aggregate);
	
	protected async Task<TAggregate> Then() {
		var instance = _registry.CreateInstance<TAggregate>();
		instance.Load(GivenEvents());
		await When(instance);
		return instance;
	}

	protected virtual Task Cleanup()
	{
		return Task.CompletedTask;
	}
}