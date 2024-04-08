namespace ProjectManagement.Presentation.Core.Api;

public interface IApiResponse
{
    public string? ErrorDescription { get; init; }
}

public interface IApiResponse<TData>
{
    public TData? Data { get; init; }
    public string? ErrorDescription { get; init; }
}
