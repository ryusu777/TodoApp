using Library.Models;

namespace AuthContext.Application.User;

public static class UserApplicationError 
{
    public static Error UserAlreadyOnboarded => new(nameof(UserAlreadyOnboarded), "The user is already registered in the system");
}
