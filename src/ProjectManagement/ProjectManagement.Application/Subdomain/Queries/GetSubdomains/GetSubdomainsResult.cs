namespace ProjectManagement.Application.Subdomain.Queries.GetSubdomains;

public class GetSubdomainsResult : List<Subdomain.Dtos.Subdomain>
{
    public GetSubdomainsResult(IEnumerable<Dtos.Subdomain> collection) : base(collection)
    {
    }
}
