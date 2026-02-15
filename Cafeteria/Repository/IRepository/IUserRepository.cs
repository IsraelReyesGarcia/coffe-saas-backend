using System;
using Cafeteria.Models;
using Cafeteria.Models.Dtos.User;

namespace Cafeteria.Repository.IRepository;

public interface IUserRepository
{

    /** 
    * Obtener todos los usuarios
    */
    ICollection<UserResponseDto> GetUsers();

    /** 
    * Obtener usuario por id
    */
    UserResponseDto GetUser(int id);

    /** 
    * Recibe el correo y revisa si es único
    */
    bool IsUniqueUser(string email);

    /** 
    * Realizar el Login del usuario
    */
    Task<LoginResponseDto> Login(UserLoginDto userLoginDto);

    /** 
    * Realizar el registro del usuario
    */
    Task<UserResponseDto> Register(CreateUserDto createUserDto);

    /** 
    * Realizar la actualización del usuario
    */
    Task<UserResponseDto> Update(User user);

    /** 
    * Confirmar cumplimiento de métodos
    */
    bool Save(); 

}
