using Domain.Shared;
using System.Diagnostics.Metrics;

namespace Domain.Errors;

public static class DomainErrors
{
    public static class GlobalErrors
    {
        public static Func<string, Error> MiddlewareErrorHandler => error => new("GlobalErrors.MiddlewareErrorHandler", error);
    }

    public static class ValueObject
    {
        public static class Email
        {
            public static readonly Error Empty = new("Email.Empty", "Email is empty");

            public static readonly Error TooLong = new("Email.TooLong", "Email is too long");

            public static readonly Error InvalidFormat = new("Email.InvalidFormat", "Email format is invalid");
        }
        public static class FirstName
        {
            public static readonly Error Empty = new("FirstName.Empty", "First Name is empty");

            public static readonly Error TooLong = new("FirstName.TooLong", "First Name is too long");
        }

        public static class LastName
        {
            public static readonly Error Empty = new("LastName.Empty", "Last Name is empty");

            public static readonly Error TooLong = new("LastName.TooLong", "Last Name is too long");
        }

        public static class Password
        {
            public static readonly Error Empty = new("Password.Empty", "Password is empty");

            public static readonly Error PasswordMeetsTheRequiredCriteria = new (
                "Password.PasswordMeetsTheRequiredCriteria",
                "Password must contain at least one uppercase letter, one lowercase letter, one '@' character, and be between 8 and 20 characters long");

            public static readonly Error NewPasswordEqualToThePreviousPassword = new(
                       "Password.NewPasswordEqualToThePreviousPassword",
                       "A new password can’t be equal to the previous password");
        }
    }
}