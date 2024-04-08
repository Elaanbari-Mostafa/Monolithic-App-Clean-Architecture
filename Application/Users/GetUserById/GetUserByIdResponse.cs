namespace Application.Users.GetUserById;

public sealed record GetUserByIdResponse(
                        string FirstName,
                        string LastName,
                        string Email,
                        DateTime DateOfBirth,
                        DateTime CreatedOnUtc,
                        DateTime ModifiedOnUtc);