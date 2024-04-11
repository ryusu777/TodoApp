using FastEndpoints;

namespace ProjectManagement.Presentation.Subdomain;

public class SubdomainEndpointGroup : Group
{
    public SubdomainEndpointGroup()
    {
        Configure(GlobalEndpointRoutes.ApiPrefix, ep => 
        { 
            ep.AllowAnonymous();
        }); 
    }
}
