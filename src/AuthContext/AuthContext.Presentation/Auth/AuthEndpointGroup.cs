using FastEndpoints;

namespace AuthContext.Presentation.Auth;

public class AuthEndpointGroup : Group
{
    public AuthEndpointGroup()
    {
        Configure(GlobalEndpointRoutes.ApiPrefix, ep => 
        { 
            ep.AllowAnonymous();
        });
    }
}
