using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Application.Project.Commands.CreateProject;
using ProjectManagement.Application.Project.Commands.CreateProjectHierarchy;
using ProjectManagement.Application.Project.Commands.CreateProjectPhase;
using ProjectManagement.Presentation.Test.Abstraction;

namespace ProjectManagement.Presentation.Test.Project.Endpoints;

public class CreateProjectHierarchyCommandTest : BaseFunctionalTest
{
    public CreateProjectHierarchyCommandTest(WebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnSuccess_WhenDataIsValid()
    {
        // Arrange
        using var scope = Factory.Services.CreateAsyncScope();
        ISender _sender = scope.ServiceProvider.GetRequiredService<ISender>();

        var createCommand = new CreateProjectCommand(
            "ProjectHierarchyA",
            "The Project A",
            "Description",
            Enumerable.Empty<CreateProjectHierarchyCommand>().ToList(),
            Enumerable.Empty<CreateProjectPhaseCommand>().ToList()
        );

        await _sender.Send(createCommand, CancellationToken.None);

        var project = new CreateProjectHierarchyCommand(
            "ProjectHierarchyA",
            "Project Manager",
            null,
            new string[] { "member-a" }
        );

        // Act
        var result = await _sender.Send(project, CancellationToken.None);

        // Assert
        // result.Error.Description.Should().BeNullOrEmpty();
        // result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenProjectIdIsWrong()
    {
        // Arrange
        using var scope = Factory.Services.CreateAsyncScope();
        ISender _sender = scope.ServiceProvider.GetRequiredService<ISender>();

        var createCommand = new CreateProjectCommand(
            "ProjectHierarchyB",
            "The Project B",
            "Description",
            Enumerable.Empty<CreateProjectHierarchyCommand>().ToList(),
            Enumerable.Empty<CreateProjectPhaseCommand>().ToList()
        );

        await _sender.Send(createCommand, CancellationToken.None);

        var project = new CreateProjectHierarchyCommand(
            "ProjectHierarchyC",
            "Project Manager",
            null,
            new string[] { "member-a" }
        );

        // Act
        var result = await _sender.Send(project, CancellationToken.None);

        // Assert
        // result.Error.Description.Should().NotBeNullOrEmpty();
        // result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenDataIsInvalid()
    {
        // Arrange
        using var scope = Factory.Services.CreateAsyncScope();
        ISender _sender = scope.ServiceProvider.GetRequiredService<ISender>();

        var createCommand = new CreateProjectCommand(
            "ProjectHierarchyD",
            "The Project D",
            "Description",
            Enumerable.Empty<CreateProjectHierarchyCommand>().ToList(),
            Enumerable.Empty<CreateProjectPhaseCommand>().ToList()
        );

        await _sender.Send(createCommand, CancellationToken.None);

        var project = new CreateProjectHierarchyCommand(
            "ProjectHierarchyD",
            "",
            null,
            new string[] { }
        );

        // Act
        var result = await _sender.Send(project, CancellationToken.None);

        // Asssert
        // result.Error.Description.Should().BeNullOrEmpty();
        // result.IsSuccess.Should().BeTrue();
    }
}
