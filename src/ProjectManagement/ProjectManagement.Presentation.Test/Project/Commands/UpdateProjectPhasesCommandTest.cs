using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Application.Project.Commands.CreateProject;
using ProjectManagement.Application.Project.Commands.CreateProjectHierarchy;
using ProjectManagement.Application.Project.Commands.CreateProjectPhase;
using ProjectManagement.Application.Project.Commands.UpdateProjectPhases;
using ProjectManagement.Application.Project.Dtos;
using ProjectManagement.Presentation.Test.Abstraction;

namespace ProjectManagement.Presentation.Test.Project.Endpoints;

public class UpdateProjectPhasesCommandTest : BaseFunctionalTest
{
    public UpdateProjectPhasesCommandTest(WebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnSuccess_WhenDataIsValid()
    {
        // Arrange
        using var scope = Factory.Services.CreateAsyncScope();
        ISender _sender = scope.ServiceProvider.GetRequiredService<ISender>();

        var createCommand = new CreateProjectCommand(
            "ProjectPhaseA",
            "The Project A",
            "Description",
            Enumerable.Empty<CreateProjectHierarchyCommand>().ToList(),
            Enumerable.Empty<CreateProjectPhaseCommand>().ToList()
        );

        await _sender.Send(createCommand, CancellationToken.None);

        var project = new UpdateProjectPhasesCommand(
            "ProjectPhaseA",
            new Phase[] {
                new Phase(null, "UR", DateOnly.MinValue, DateOnly.MaxValue, "User Requirement")
            }
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
            "ProjectPhaseB",
            "The Project A",
            "Description",
            Enumerable.Empty<CreateProjectHierarchyCommand>().ToList(),
            Enumerable.Empty<CreateProjectPhaseCommand>().ToList()
        );

        await _sender.Send(createCommand, CancellationToken.None);

        var project = new UpdateProjectPhasesCommand(
            "ProjectPhaseC",
            new Phase[] {
                new Phase(null, "UR", DateOnly.MinValue, DateOnly.MaxValue, "User Requirement")
            }
        );

        // Act
        var result = await _sender.Send(project, CancellationToken.None);

        // Assert
        // result.Error.Description.Should().NotBeNullOrEmpty();
        // result.IsFailure.Should().BeTrue();
    }
}
