using Library.Models;

namespace ProjectManagement.Infrastructure.Subdomain;

public static class SubdomainInfrastructureError
{
	public static Error SubdomainNotFound => new Error(nameof(SubdomainNotFound), "The subdomain is not found");
}
