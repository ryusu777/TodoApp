using FastEndpoints;

namespace ProjectManagement.Presentation.Project;

public class ProjectEndpointGroup : Group
{
    public ProjectEndpointGroup()
    {
        Configure(GlobalEndpointRoutes.ApiPrefix, ep => 
        {
            ep.AllowAnonymous();
        }); 
    }
}
