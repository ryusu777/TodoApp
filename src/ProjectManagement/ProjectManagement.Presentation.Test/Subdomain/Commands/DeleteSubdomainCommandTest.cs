using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Application.Project.Commands.CreateProject;
using ProjectManagement.Application.Project.Commands.CreateProjectHierarchy;
using ProjectManagement.Application.Project.Commands.CreateProjectPhase;
using ProjectManagement.Application.Project.Dtos;
using ProjectManagement.Application.Subdomain.Commands.DeleteSubdomain;
using ProjectManagement.Presentation.Test.Abstraction;

namespace ProjectManagement.Presentation.Test.Project.Endpoints;

public class DeleteSubdomainCommandTest : BaseFunctionalTest
{
    public DeleteSubdomainCommandTest(WebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnSuccess_WhenDataIsValid()
    {
        // Arrange
        using var scope = Factory.Services.CreateAsyncScope();
        ISender _sender = scope.ServiceProvider.GetRequiredService<ISender>();

        var createCommand = new CreateProjectCommand(
            "ProjectSubdomainA",
            "The Project A",
            "Description",
            Enumerable.Empty<CreateProjectHierarchyCommand>().ToList(),
            Enumerable.Empty<CreateProjectPhaseCommand>().ToList()
        );

        // await _sender.Send(createCommand, CancellationToken.None);

        var project = new DeleteSubdomainCommand(
            Guid.NewGuid());

        // Act
        var result = await _sender.Send(project);

        // Assert
        result.Error.Description.Should().NotBeNullOrEmpty();
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenProjectIdIsWrong()
    {
        // Arrange
        using var scope = Factory.Services.CreateAsyncScope();
        ISender _sender = scope.ServiceProvider.GetRequiredService<ISender>();

        var createCommand = new CreateProjectCommand(
            "ProjectSubdomainB",
            "The Project B",
            "Description",
            Enumerable.Empty<CreateProjectHierarchyCommand>().ToList(),
            Enumerable.Empty<CreateProjectPhaseCommand>().ToList()
        );

        await _sender.Send(createCommand, CancellationToken.None);

        var project = new DeleteSubdomainCommand(
            Guid.NewGuid());

        // Act
        var result = await _sender.Send(project, CancellationToken.None);

        // Assert
        result.Error.Description.Should().NotBeNullOrEmpty();
        result.IsFailure.Should().BeTrue();
    }
}
