using Domain.Shared;

namespace Domain.Errors;

public static class DomainErrors
{
    public static class GlobalErrors
    {
        public static Func<Exception, Error> MiddlewareErrorHandler => ex => new("GlobalErrors.MiddlewareErrorHandler", ex.Message);
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

            public static readonly Error PasswordMeetsTheRequiredCriteria = new(
                "Password.PasswordMeetsTheRequiredCriteria",
                "Password must contain at least one uppercase letter, one lowercase letter, one '@' character, and be between 8 and 20 characters long");

            public static readonly Error NewPasswordEqualToThePreviousPassword = new(
                       "Password.NewPasswordEqualToThePreviousPassword",
                       "A new password can’t be equal to the previous password");
        }

        public static class Money
        {
            public static readonly Error ThePriceIsNegatif = new("Money.ThePriceIsNegatif", "The Price Is Negatif !");
        }
    }

    public static class User
    {
        public static readonly Func<Guid, Error> UserWithIdNotFound = id => new("User.UserWithIdNotFound", $"User with this id {id} is not found!");

        public static readonly Func<string, Error> UserWithEmailNotFound = email => new("User.UserWithEmailNotFound", $"User with this email {email} is not found!");

        public static readonly Error EmailAlreadyInUse = new("User.EmailAlreadyInUse", "The specified email is already in use");

        public static readonly Error InvalidCredentials = new("User.InvalidCredentials", "The provided credentials are invalid");

        public static readonly Func<int, Error> RoleNotFound = id => new("User.RoleNotFound", $"The role with this id = {id} is not found");
    }

    public static class Role
    {
        public static Func<int, Error> RoleNotFound => id => new("Role.RoleNotFound", $"This Role '{id}' is not found");
    }

    public static class Brand
    {
        public static readonly Func<Guid, Error> BrandNotFound = id =>
            new("Brand.BrandNotFound", $"This Brand {id} is not found");
    }

    public static class Product
    {
        public static Func<IEnumerable<Guid>, Error> ProductIdsNotFound => ids => new(
            "Product.ProductIdsNotFound",
            $"Product(s) with ID(s) {string.Join(", ", ids)} not found.");

        public static Func<Guid, Error> ProductNotFound => id => new("Product.ProductNotFound", $"This Product '{id}' is not found");
    }

    public static class Jwt
    {
        public static readonly Error TokenNotFoundInTheRequestHeaders = new(
            "Jwt.TokenNotFoundInTheRequestHeaders",
            "Authorization token not found in the request headers.");

        public static readonly Func<string, Error> TokenNotFoundOrInvalide = fieldName => new(
            "Jwt.TokenNotFoundOrInvalide",
            $"'{fieldName}' claim not found in the token or invalid.");

        public static readonly Func<string, Error> TypeOfTokenIsInvalide = fieldType => new(
            "Jwt.TypeOfTokenIsInvalide",
             $"'The type of {fieldType}' is invalid.");
    }

    public static class Order
    {
        public static Func<Guid, Error> OrderNotFound => id => new("Order.OrderNotFound", $"This Order '{id}' is not found");

        public static Func<Guid, Error> ThisOrderIsNotPending => id => new("Order.ThisOrderIsNotPending", $"This Order '{id}' is not Pending");
    }
}