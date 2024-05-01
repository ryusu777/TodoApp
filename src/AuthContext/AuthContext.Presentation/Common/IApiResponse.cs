namespace AuthContext.Presentation.Common;

public interface IApiResponse
{
    public string? ErrorDescription { get; init; }
    public string? ErrorCode { get; init; }
}

public interface IApiResponse<TData>
{
    public string? ErrorDescription { get; init; }
    public string? ErrorCode { get; init; }
    public TData? Data { get; init; }
}
