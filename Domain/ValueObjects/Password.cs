using Domain.Errors;
using Domain.Primitives;
using Domain.Shared;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public sealed class Password : ValueObject
{
    public const int MinLength = 8;
    public const int MaxLength = 20;
    private static readonly Regex PasswordRegex = new($"^(?=.*[a-z])(?=.*[A-Z])(?=.*[@])[^\\s]{{{MinLength},{MaxLength}}}$");

    public string Value { get; private init; }

    internal Password(string value) => Value = value;

    public static Result<Password> Create(string value)
        => Result.Create(value)
                    .Ensure(string.IsNullOrWhiteSpace, DomainErrors.ValueObject.Password.Empty)
                    .Ensure(p => !PasswordRegex.IsMatch(p), DomainErrors.ValueObject.Password.PasswordMeetsTheRequiredCriteria)
                    .Map(p => new Password(HashPassword(p)));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    private static string HashPassword(string password)
        => BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));

    public bool VerifyPassword(string password)
        => BCrypt.Net.BCrypt.Verify(Value, password);

    private static bool IsBcryptHash(string hashedValue)
    {
        // BCrypt hashes start with "$2a$", "$2b$", "$2y$", or "$2x$"
        return hashedValue.StartsWith("$2a$") || hashedValue.StartsWith("$2b$") || hashedValue.StartsWith("$2y$") || hashedValue.StartsWith("$2x$");
    }

    public static Password FromHashedString(string hashedValue)
         => IsBcryptHash(hashedValue)
                ? new(hashedValue)
                : throw new InvalidOperationException("The string given is not hashed !!!");
}