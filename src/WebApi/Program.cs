using BasketballGame.Application;
using BasketballGame.Domain;
using Eventuous;
using Eventuous.EventStore;
using Eventuous.Projections.MongoDB;
using Serilog;
using WebApi;

var serilogBuilder =
	new LoggerConfiguration()
		.MinimumLevel.Verbose()
		.Enrich.FromLogContext()
		.WriteTo.Console();

Log.Logger = serilogBuilder.CreateLogger();

var builder = WebApplication.CreateBuilder(args);

try
{
	Log.Information("Starting application");
	builder.Host.UseSerilog();
	var configuration = builder.Configuration;
	var services = builder.Services;
	ConfigureServices(services, configuration);
	var app = builder.Build();
	Configure(app);
	await app.RunAsync();
}
catch (Exception exception)
{
	Log.Fatal(exception, "Application terminated unexpectedly");
}
finally
{
	await Log.CloseAndFlushAsync();
}

return;

static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
	TypeMap.RegisterKnownEventTypes(typeof(Game).Assembly);
	
	services
		.AddEventStoreClient(configuration["EventStore:ConnectionString"]!)
		.AddAggregateStore<EsdbEventStore>()
		.AddCommandService<GameCommandService, Game>()
		.AddSingleton(MongoClientFactory.ConfigureMongo(configuration))
		.AddCheckpointStore<MongoCheckpointStore>()
		.AddOpenApi()
		.AddEndpointsApiExplorer()
		.AddControllers();

}

static void Configure(WebApplication app)
{
	var configuration = app.Configuration;
	var serviceProvider = app.Services;
	var assemblyPath = $"/{typeof(Program).Assembly.GetName().Name!.ToLowerInvariant()}";

	app.AddClientCommands();
	app.UsePathBase(assemblyPath);
	app.UseOpenApi(assemblyPath);
	app.MapGet("/", () => "Hello World!");
	app.MapControllers();
}