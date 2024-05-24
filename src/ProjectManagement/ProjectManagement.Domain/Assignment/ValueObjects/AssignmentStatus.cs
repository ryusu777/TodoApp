using Library.Models;
using ProjectManagement.Domain.Assignment.Enums;

namespace ProjectManagement.Domain.Assignment.ValueObjects;

public record AssignmentStatus(AssignmentStatusEnum Value) : IEquatable<AssignmentStatusEnum>
{
    public static Result<AssignmentStatus> Create(string value) 
    {
        bool result = Enum
            .TryParse(value, out AssignmentStatusEnum enumResult);
        if (!result)
            return Result
                .Failure<AssignmentStatus>(AssignmentDomainErrors
                    .InvalidAssignmentStatus(value)
                );

        return Result.Success(new AssignmentStatus(enumResult));
    }

    public bool Equals(AssignmentStatusEnum other)
    {
        return Value == other;
    }

    public static bool operator==(AssignmentStatus left, AssignmentStatusEnum right)
    {
        return Equals(left, right);
    }

    public static bool operator!=(AssignmentStatus left, AssignmentStatusEnum right)
    {
        return !Equals(left, right);
    }
}
