using AutoMapper;
using Cafeteria.Models;
using Cafeteria.Models.Dtos.User;

namespace Cafeteria.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        // CreateUserDto → User (Ignorar Password, se hashea en el Service)
        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // ✅ Se maneja en Service
            .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Role, opt => opt.Ignore()); // Se carga por EF

        // UpdateUserDto → User (Actualizar propiedades existentes)
        CreateMap<UserUpdateDto, User>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // ✅ NO se actualiza aquí
            .ForMember(dest => dest.CreateAt, opt => opt.Ignore()) // Mantener fecha original
            .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Role, opt => opt.Ignore());

        // User → UserResponseDto (Incluir datos del Role)
        CreateMap<User, UserResponseDto>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role != null ? src.Role.name : string.Empty));

        // User → LoginResponseDto (Respuesta después de login)
        CreateMap<User, LoginResponseDto>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role != null ? src.Role.name : string.Empty))
            .ForMember(dest => dest.Token, opt => opt.Ignore()) // ✅ Se genera en Service
            .ForMember(dest => dest.TokenExpiration, opt => opt.Ignore()); // ✅ Se genera en Service

    }
}