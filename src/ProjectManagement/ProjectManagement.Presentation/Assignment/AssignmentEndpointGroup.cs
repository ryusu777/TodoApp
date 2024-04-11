using FastEndpoints;

namespace ProjectManagement.Presentation.Assignment;

public class AssignmentEndpointGroup : Group
{
    public AssignmentEndpointGroup()
    {
        Configure(GlobalEndpointRoutes.ApiPrefix, ep => 
        { 
            ep.AllowAnonymous();
        }); 
    }
}
