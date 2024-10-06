using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Application.Project.Commands.CreateProject;
using ProjectManagement.Application.Project.Commands.CreateProjectHierarchy;
using ProjectManagement.Application.Project.Commands.CreateProjectPhase;
using ProjectManagement.Application.Project.Dtos;
using ProjectManagement.Application.Subdomain.Commands.CreateSubdomain;
using ProjectManagement.Presentation.Test.Abstraction;

namespace ProjectManagement.Presentation.Test.Project.Endpoints;

public class CreateSubdomainCommandTest : BaseFunctionalTest
{
    public CreateSubdomainCommandTest(WebAppFactory factory) : base(factory)
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

        var project = new CreateSubdomainCommand(
            "SubdomainA",
            "Description",
            "ProjectSubdomainA");

        // Act
        var result = await _sender.Send(new CreateSubdomainCommand(
            "SubdomainA",
            "Description",
            "ProjectSubdomainA"), CancellationToken.None);

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

        var project = new CreateSubdomainCommand(
            "",
            "",
            "ProjectSubdomainA");

        // Act
        var result = await _sender.Send(project, CancellationToken.None);

        // Asssert
        result.Error.Description.Should().BeNullOrEmpty();
        result.IsFailure.Should().BeTrue();
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

        var project = new CreateSubdomainCommand(
            "SubdomainB",
            "Description",
            "ProjectSubdomainC");

        // Act
        var result = await _sender.Send(project, CancellationToken.None);

        // Assert
        result.Error.Description.Should().NotBeNullOrEmpty();
        result.IsFailure.Should().BeTrue();
    }
}
