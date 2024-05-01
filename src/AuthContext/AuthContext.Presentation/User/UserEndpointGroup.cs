using FastEndpoints;

namespace AuthContext.Presentation.User;

public class UserEndpointGroup : Group
{
    public UserEndpointGroup()
    {
        Configure(GlobalEndpointRoutes.ApiPrefix, ep => 
        { 
            ep.AllowAnonymous();
        });
    }
}
