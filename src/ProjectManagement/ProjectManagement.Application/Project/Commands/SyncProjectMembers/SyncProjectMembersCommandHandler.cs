using Library.Models;
using MassTransit;
using MassTransitContracts.ProjectManagement.Members.GetAllAssignee;
using ProjectManagement.Application.Abstractions.Data;
using ProjectManagement.Application.Abstractions.Messaging;
using ProjectManagement.Domain.Common.ValueObjects;
using ProjectManagement.Domain.Project;
using ProjectManagement.Domain.Project.ValueObjects;

namespace ProjectManagement.Application.Project.Commands.SyncProjectMembers;

public class SyncProjectMembersCommandHandler : ICommandHandler<SyncProjectMembersCommand>
{
    private readonly IRequestClient<GetAllAssigneeRequest> _client;
    private readonly IProjectRepository _projectRepo;
    private readonly IUnitOfWork _unitOfWork;

    public SyncProjectMembersCommandHandler(IRequestClient<GetAllAssigneeRequest> client, IProjectRepository projectRepo, IUnitOfWork unitOfWork)
    {
        _client = client;
        _projectRepo = projectRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(SyncProjectMembersCommand request, CancellationToken cancellationToken)
    {
        var projectResult = await _projectRepo.GetProjectById(ProjectId.Create(request.ProjectId), cancellationToken);
        if (projectResult.IsFailure || projectResult.Value is null)
            return projectResult.Error;

        var project = projectResult.Value;

        try 
        {
            var response = await _client
                .GetResponse<GetAllAssigneeResponse>(
                    new GetAllAssigneeRequest(request.ProjectId, request.UserId), 
                    cancellationToken);

            project
                .UpdateProjectMembers(response
                    .Message
                    .Assignees
                    .Select(e => UserId.Create(e))
                    .ToList());

            return await _unitOfWork.SaveChangesAsync(project.DomainEvents, cancellationToken);
        }
        catch (Exception e)
        {
            return ProjectDomainErrors.FailedToSyncMembers(e.Message);
        }
    }
}

