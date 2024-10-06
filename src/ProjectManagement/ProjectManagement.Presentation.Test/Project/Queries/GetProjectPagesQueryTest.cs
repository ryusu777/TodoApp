using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Application.Project.Commands.CreateProject;
using ProjectManagement.Application.Project.Commands.CreateProjectHierarchy;
using ProjectManagement.Application.Project.Commands.CreateProjectPhase;
using ProjectManagement.Application.Project.Queries.GetProjectPages;
using ProjectManagement.Presentation.Test.Abstraction;

namespace ProjectManagement.Presentation.Test.Project.Endpoints;

public class GetProjectPagesQueryTest : BaseFunctionalTest
{
    public GetProjectPagesQueryTest(WebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnSuccess_WhenDataIsValid()
    {
        // Arrange
        using var scope = Factory.Services.CreateAsyncScope();
        ISender _sender = scope.ServiceProvider.GetRequiredService<ISender>();

        var project = new CreateProjectCommand(
            "ProjectA",
            "The Project A",
            "DescriptionA",
            Enumerable.Empty<CreateProjectHierarchyCommand>().ToList(),
            Enumerable.Empty<CreateProjectPhaseCommand>().ToList()
        );
        await _sender.Send(project, CancellationToken.None);

        // Act
        var result = await _sender.Send(new GetProjectPagesQuery(), CancellationToken.None);

        // Assert
        result.Error.Description.Should().BeNullOrEmpty();
        result.IsSuccess.Should().BeTrue();
    }
}
