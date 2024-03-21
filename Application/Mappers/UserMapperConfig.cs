using Application.Users.GetUserById;
using Domain.Entities;
using Mapster;

namespace Application.Mappers;

internal sealed class UserMapperConfig : IRegister
{
    void IRegister.Register(TypeAdapterConfig config)
         => config.NewConfig<User, GetUserByIdResponse>()
                .Map(dest => dest.FirstName, src => src.FirstName.Value)
                .Map(dest => dest.LastName, src => src.LastName.Value)
                .Map(dest => dest.Email, src => src.Email.Value);
}