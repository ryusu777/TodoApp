using Library.Models;

namespace IntegrationContext.Domain.Hooks;

public static class HooksDomainError
{
    public static Error FailedToCreateAssignment(string message)
        => new(nameof(FailedToCreateAssignment), message);
}
