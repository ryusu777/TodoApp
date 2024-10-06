using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Application.Project.Commands.CreateProject;
using ProjectManagement.Application.Project.Commands.CreateProjectHierarchy;
using ProjectManagement.Application.Project.Commands.CreateProjectPhase;
using ProjectManagement.Presentation.Test.Abstraction;

namespace ProjectManagement.Presentation.Test.Project.Endpoints;

public class CreateProjectCommandTest : BaseFunctionalTest
{
    public CreateProjectCommandTest(WebAppFactory factory) : base(factory)
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

        var project = new CreateProjectCommand(
            "ProjectB",
            "The Project B",
            "Description",
            Enumerable.Empty<CreateProjectHierarchyCommand>().ToList(),
            Enumerable.Empty<CreateProjectPhaseCommand>().ToList()
        );

        // Act
        var result = await _sender.Send(project, CancellationToken.None);

        // Asssert
        result.Error.Description.Should().BeNullOrEmpty();
        result.IsFailure.Should().BeFalse();
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenProjectNameInvalid()
    {
        // Arrange
        using var scope = Factory.Services.CreateAsyncScope();
        ISender _sender = scope.ServiceProvider.GetRequiredService<ISender>();

        var project = new CreateProjectCommand(
            "ProjectC",
            "The Project C The Project C The Project C",
            "Description",
            Enumerable.Empty<CreateProjectHierarchyCommand>().ToList(),
            Enumerable.Empty<CreateProjectPhaseCommand>().ToList()
        );

        // Act
        var result = await _sender.Send(project, CancellationToken.None);

        // Assert
        result.Error.Description.Should().BeNullOrEmpty();
        result.IsFailure.Should().BeFalse();
    }
}
