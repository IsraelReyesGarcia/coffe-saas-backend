using System;
using Cafeteria.Models;
using Cafeteria.Models.Dtos.User;

namespace Cafeteria.Repository.IRepository;

public interface IUserRepository
{

    /** 
    * Obtener todos los usuarios
    */
    ICollection<ApplicationUser> GetUsers();

    /** 
    * Obtener usuario por id
    */
    ApplicationUser GetUser(string id);

    /** 
    * Recibe el correo y revisa si es único
    */
    bool IsUniqueUser(string email);

    /** 
    * Realizar el Login del usuario
    */
    Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto);

    /** 
    * Realizar el registro del usuario
    */
    Task<UserDataDto> Register(CreateUserDto createUserDto);

    /** 
    * Realizar la actualización del usuario
    */
    //Task<ApplicationUser> Update(User user);

    /** 
    * Confirmar cumplimiento de métodos
    */

    /* 
        Actualiza el usuario
     */
    Task<UserDataDto?> Update(string id, UserUpdateDto userUpdateDto);

    bool Save(); 

}
