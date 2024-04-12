using Library.Models;
using ProjectManagement.Application.Abstractions.Messaging;

namespace ProjectManagement.Application.Project.Queries.GetProjectPages;

public class GetProjectPagesHandler : IQueryHandler<GetProjectPagesQuery, IEnumerable<GetProjectPagesResult>>
{
    private readonly IProjectRepository _repository;

    public GetProjectPagesHandler(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<GetProjectPagesResult>>> Handle(GetProjectPagesQuery query, CancellationToken cancellationToken)
    {
        return await _repository.GetProjectPages(cancellationToken);
    }
}

