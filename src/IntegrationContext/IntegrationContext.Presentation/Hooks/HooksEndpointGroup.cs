using FastEndpoints;
using ProjectManagement.Presentation;

namespace IntegrationContext.Presentation.Hooks;

public class HooksEndpointGroup : Group
{
    public HooksEndpointGroup()
    {
        Configure(GlobalEndpointRoutes.ApiPrefix, ep => 
        { 
            ep.AllowAnonymous();
        });
    }
}
