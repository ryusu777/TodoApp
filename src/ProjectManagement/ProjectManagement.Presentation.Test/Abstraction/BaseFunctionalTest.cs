namespace ProjectManagement.Presentation.Test.Abstraction;

public class BaseFunctionalTest : IClassFixture<WebAppFactory>
{
	protected HttpClient HttpClient { get; init; }
	public BaseFunctionalTest(WebAppFactory factory)
	{
		HttpClient = factory.CreateClient();
	}
}
