namespace WebApi;

public static class Middlewares
{
	public static WebApplication UseOpenApi(this WebApplication app, string assemblyPath)
	{
		var openApiRelativePath = $"{assemblyPath}/swagger/v1/swagger.json";
		const string openApiName = "Web Api";

		app.UseSwagger();
		app.UseSwaggerUI(options => options.SwaggerEndpoint(openApiRelativePath, openApiName));

		return app;
	}

}