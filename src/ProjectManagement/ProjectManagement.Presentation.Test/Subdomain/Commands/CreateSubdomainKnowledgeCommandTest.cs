using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Application.Project.Commands.CreateProject;
using ProjectManagement.Application.Project.Commands.CreateProjectHierarchy;
using ProjectManagement.Application.Project.Commands.CreateProjectPhase;
using ProjectManagement.Application.Project.Dtos;
using ProjectManagement.Application.Subdomain.Commands.CreateSubdomainKnowledge;
using ProjectManagement.Presentation.Test.Abstraction;

namespace ProjectManagement.Presentation.Test.Project.Endpoints;

public class CreateSubdomainKnowledgeCommandTest : BaseFunctionalTest
{
    public CreateSubdomainKnowledgeCommandTest(WebAppFactory factory) : base(factory)
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

        var project = new CreateSubdomainKnowledgeCommand(
            "Title",
            "Content",
            Guid.NewGuid());

        // Act
        var result = await _sender.Send(project);

        // Asssert
        result.Error.Description.Should().NotBeNullOrEmpty();
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenDataIsInvalid()
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

        var project = new CreateSubdomainKnowledgeCommand(
            "",
            "",
            Guid.NewGuid());

        // Act
        var result = await _sender.Send(project);

        // Asssert
        result.Error.Description.Should().NotBeNullOrEmpty();
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenSubdomainIdIsWrong()
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

        var project = new CreateSubdomainKnowledgeCommand(
            "Title",
            "Content",
            Guid.NewGuid());

        // Act
        var result = await _sender.Send(project);

        // Asssert
        result.Error.Description.Should().NotBeNullOrEmpty();
        result.IsFailure.Should().BeTrue();
    }
}
