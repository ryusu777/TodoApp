using FluentAssertions;
using ProjectManagement.Application.Project.Commands.CreateProjectPhase;
using ProjectManagement.Presentation.Project;
using ProjectManagement.Presentation.Project.Endpoints.CreateProject;
using ProjectManagement.Presentation.Test.Abstraction;
using System.Net;
using System.Net.Http.Json;

namespace ProjectManagement.Presentation.Test.Project;

public class CreateProjectTests : BaseFunctionalTest
{
	public CreateProjectTests(WebAppFactory factory) : base(factory)
	{
	}

	[Fact]
	public async Task Should_ReturnNoContent_WhenContractIsValid()
	{
		// arrange
		var request = new CreateProjectRequest(
			"TodoApp",
			"Todo Application",
			"An application that manages todo app",
			["ryusu777"],
			[
				new CreateProjectPhaseCommand(
					"Iteration 1", 
					DateOnly.Parse("2024-01-12"),
					DateOnly.Parse("2024-01-20"),
					"Requirement Gathering"
				)
			]);

		// act
		HttpResponseMessage response = await HttpClient
			.PostAsJsonAsync("api" + ProjectEndpointRoutes.Project, request);

		// assert
		response.StatusCode.Should().Be(HttpStatusCode.NoContent);
	}
}
