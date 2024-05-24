using System;

namespace Library.Models;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? other)
    {
        if (other == null || other.GetType() != GetType()) 
        {
            return false;
        }

        var valueObject = (ValueObject)other;

        return GetEqualityComponents()
            .SequenceEqual(valueObject.GetEqualityComponents());
    }

    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(e => e?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }

    public bool Equals(ValueObject? other)
    {
        return Equals(other);
    }
}
