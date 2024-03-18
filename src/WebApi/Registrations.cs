namespace WebApi;

public static class Registrations
{
	public static IServiceCollection AddOpenApi(this IServiceCollection services)
	{
		services.AddSwaggerGen();

		return services;
	}
}