using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.Application.Project.Commands.CreateProject;
using ProjectManagement.Application.Project.Commands.CreateProjectHierarchy;
using ProjectManagement.Application.Project.Commands.CreateProjectPhase;
using ProjectManagement.Application.Subdomain.Queries.GetSubdomain;
using ProjectManagement.Presentation.Test.Abstraction;

namespace ProjectManagement.Presentation.Test.Project.Endpoints;

public class GetSubdomainQueryTest : BaseFunctionalTest
{
    public GetSubdomainQueryTest(WebAppFactory factory) : base(factory)
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
        var result = await _sender.Send(new GetSubdomainQuery(Guid.NewGuid()), CancellationToken.None);

        // Asssert
        result.Error.Description.Should().NotBeNullOrEmpty();
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenSubddomainIsNotFound()
    {
        // Arrange
        using var scope = Factory.Services.CreateAsyncScope();
        ISender _sender = scope.ServiceProvider.GetRequiredService<ISender>();

        // Act
        var result = await _sender.Send(new GetSubdomainQuery(Guid.NewGuid()), CancellationToken.None);

        // Asssert
        result.Error.Description.Should().NotBeNullOrEmpty();
        result.IsFailure.Should().BeTrue();
    }
}
