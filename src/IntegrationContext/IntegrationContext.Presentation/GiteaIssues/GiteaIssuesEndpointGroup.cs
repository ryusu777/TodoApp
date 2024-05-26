using FastEndpoints;
using ProjectManagement.Presentation;

namespace IntegrationContext.Presentation.GiteaIssues;

public class GiteaIssuesEndpointGroup : Group
{
    public GiteaIssuesEndpointGroup()
    {
        Configure(GlobalEndpointRoutes.ApiPrefix, ep => 
        { }); 
    }
}
