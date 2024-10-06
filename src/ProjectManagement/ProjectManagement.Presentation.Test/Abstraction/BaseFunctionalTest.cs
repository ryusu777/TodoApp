using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Infrastructure.Persistence.Data;

namespace ProjectManagement.Presentation.Test.Abstraction;

public class BaseFunctionalTest : IClassFixture<WebAppFactory>
{
	protected HttpClient HttpClient { get; init; }
    protected WebAppFactory Factory { get; init; }
	public BaseFunctionalTest(WebAppFactory factory)
	{
		HttpClient = factory.CreateClient();
        Factory = factory;
        var scope = Factory.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
	}
}
