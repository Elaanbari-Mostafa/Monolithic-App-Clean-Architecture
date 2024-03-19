namespace Application.Users.GetUserById;

public record GetUserByIdResponse(
                        string FirstName,
                        string LastName,
                        string Email,
                        int UserType,
                        DateTime DateOfBirth,
                        DateTime CreatedOnUtc,
                        DateTime ModifiedOnUtc);