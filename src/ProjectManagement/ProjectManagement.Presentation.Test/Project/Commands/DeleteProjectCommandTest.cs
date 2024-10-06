using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Application.Project.Commands.CreateProject;
using ProjectManagement.Application.Project.Commands.CreateProjectHierarchy;
using ProjectManagement.Application.Project.Commands.CreateProjectPhase;
using ProjectManagement.Application.Project.Commands.DeleteProject;
using ProjectManagement.Presentation.Test.Abstraction;

namespace ProjectManagement.Presentation.Test.Project.Endpoints;

public class DeleteProjectCommandTest : BaseFunctionalTest
{
    public DeleteProjectCommandTest(WebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnSuccess_WhenDataIsValid()
    {
        // Arrange
        using var scope = Factory.Services.CreateAsyncScope();
        ISender _sender = scope.ServiceProvider.GetRequiredService<ISender>();

        var createCommand = new CreateProjectCommand(
            "ProjectA",
            "The Project A",
            "Description",
            Enumerable.Empty<CreateProjectHierarchyCommand>().ToList(),
            Enumerable.Empty<CreateProjectPhaseCommand>().ToList()
        );

        await _sender.Send(createCommand, CancellationToken.None);

        var project = new DeleteProjectCommand(
            "ProjectA"
        );

        // Act
        var result = await _sender.Send(project, CancellationToken.None);

        // Assert
        result.Error.Description.Should().BeNullOrEmpty();
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenDataIsInvalid()
    {
        // Arrange
        using var scope = Factory.Services.CreateAsyncScope();
        ISender _sender = scope.ServiceProvider.GetRequiredService<ISender>();

        var createCommand = new CreateProjectCommand(
            "ProjectA",
            "The Project A",
            "Description",
            Enumerable.Empty<CreateProjectHierarchyCommand>().ToList(),
            Enumerable.Empty<CreateProjectPhaseCommand>().ToList()
        );

        await _sender.Send(createCommand, CancellationToken.None);

        var project = new DeleteProjectCommand(
            ""
        );

        // Act
        var result = await _sender.Send(project, CancellationToken.None);

        // Assert
        result.Error.Description.Should().NotBeNullOrEmpty();
        result.IsFailure.Should().BeTrue();
    }
}
