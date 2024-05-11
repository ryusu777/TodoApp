using FastEndpoints;
using ProjectManagement.Presentation;

namespace IntegrationContext.Presentation.Project;

public class GiteaRepositoriesEndpointGroup : Group
{
    public GiteaRepositoriesEndpointGroup()
    {
        Configure(GlobalEndpointRoutes.ApiPrefix, ep => 
        { }); 
    }
}
