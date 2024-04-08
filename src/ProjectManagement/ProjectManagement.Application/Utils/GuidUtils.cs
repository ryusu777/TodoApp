using Library.Models;

namespace ProjectManagement.Application.Utils;

public static class GuidUtils
{
    public static Result<Guid> ToGuid(this string str)
    {
        bool isValid = Guid.TryParse(str, out Guid result);

        if (!isValid)
        {
            return Result.Failure<Guid>(new Error("InvalidGuid", "Invalid Guid"));
        }

        return Result.Success(result);
    }

    public static Result<ICollection<Guid>> ToGuid(this ICollection<string> strs)
    {
        ICollection<Guid> results = new List<Guid>(strs.Count);
        for (int i = 0; i < strs.Count; i++)
        {
            Result<Guid> result = strs.ElementAt(i).ToGuid();
            if (result.IsFailure)
                return Result.Failure<ICollection<Guid>>(result.Error);
            results.Add(result.Value);
        }

        return Result.Success(results);
    }
}
