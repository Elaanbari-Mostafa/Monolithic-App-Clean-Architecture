﻿namespace Application.Users.Register;

public sealed record RegisterCommand(
                        string FirstName,
                        string LastName,
                        string Email,
                        string Password,
                        DateTime DateOfBirth) : ICommand<Guid>;
