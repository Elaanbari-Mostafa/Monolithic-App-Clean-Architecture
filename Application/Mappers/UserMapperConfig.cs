using Application.Users.GetUserById;

namespace Application.Mappers;

internal sealed class UserMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
         => config.NewConfig<User, GetUserByIdResponse>()
                .Map(dest => dest.FirstName, src => src.FirstName.Value)
                .Map(dest => dest.LastName, src => src.LastName.Value)
                .Map(dest => dest.Email, src => src.Email.Value);
}