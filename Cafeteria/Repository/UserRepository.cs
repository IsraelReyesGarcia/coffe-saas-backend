using System;
using Cafeteria.Models.Dtos.User;
using Cafeteria.Repository.IRepository;
using Cafeteria.Models;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Cafeteria.Models.Enums;

namespace Cafeteria.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;
    private string? secretKey;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IMapper _mapper;

    public UserRepository(ApplicationDbContext db, IConfiguration configuration, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IMapper mapper)
    {   
        _db = db;
        secretKey = configuration.GetValue<string>("ApiSettings:SecretKey");
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public ApplicationUser GetUser(string id)
    {
        return _db.ApplicationUsers.FirstOrDefault(u => u.Id == id);
        
       /*  if (result == null)
        {
            return null!;
        }

        return new UserResponseDto {
            UserId = result.UserId,
            Name = result.Name,
            Phone = result.Phone,
            Email = result.Email,
            RoleId = result.RoleId,
            RoleName = result.Role.name,
            CreateAt = result.CreateAt,
            UpdateAt = result.UpdateAt
        }; */
    }

    public ICollection<ApplicationUser> GetUsers()
    {
        return _db.ApplicationUsers.OrderBy(u => u.UserName).ToList();
            //.Include(u => u.Role)
            //.OrderBy(u => u.Name)
            /* .Select(u => new UserResponseDto
            {
                UserId = u.UserId,
                Name = u.Name,
                Phone = u.Phone,
                Email = u.Email,
                RoleId = u.RoleId,
                RoleName = u.Role.name,
                CreateAt = u.CreateAt,
                UpdateAt = u.UpdateAt
            }).ToList(); */
    }

    public bool IsUniqueUser(string email)
    {
        //return _db.Users.Any(p =>p.Email.ToLower().Trim() == email.ToLower().Trim());
        return _db.ApplicationUsers.Any(u => u.Email != null &&  u.Email.ToLower().Trim() == email.ToLower().Trim());
    }

    public async Task<UserDataDto> Register(CreateUserDto createUserDto)
    {
        /* var role = await _db.Roles.FindAsync(createUserDto.RoleId);
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);

        var user = new Cafeteria.Models.User
        {
            Name = createUserDto.Name,
            Phone = createUserDto.Phone,
            Email = createUserDto.Email,
            PasswordHash = passwordHash,
            RoleId = createUserDto.RoleId,
            CreateAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
            UpdateAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
            Role = role!
        };

        Console.WriteLine($"New User is: {user}");

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        return new UserResponseDto
        {
            UserId = user.UserId,
            Name = user.Name,
            Phone = user.Phone,
            Email = user.Email,
            RoleId = user.RoleId,
            RoleName = role?.name ?? string.Empty,
            CreateAt = user.CreateAt,
            UpdateAt = user.UpdateAt
        }; */
        if (string.IsNullOrEmpty(createUserDto.Username))
        {
            throw new ArgumentNullException("El username es requerido");
        }

        if (createUserDto.Password == null)
        {
            throw new ArgumentNullException("El password es requerido");
        }

        if(createUserDto.Email == null)
        {
            throw new ArgumentNullException("El email es requerido");
        }


        var user = new ApplicationUser()
        {
            UserName = createUserDto.Username,
            Email = createUserDto.Email,
            NormalizedEmail = createUserDto.Email.ToUpper(),
            Name = createUserDto.Name
        };

        var result = await _userManager.CreateAsync(user, createUserDto.Password);
        if (result.Succeeded)
        {
            var userRole = createUserDto.Role ?? "User";
            var roleExists = await _roleManager.RoleExistsAsync(userRole);
            if (!roleExists)
            {
                var identityRole = new ApplicationRole { Name = userRole };
                await _roleManager.CreateAsync(identityRole);                                                                              
            }
            await _userManager.AddToRoleAsync(user, userRole);

            var createUser = _db.ApplicationUsers.FirstOrDefault(u => u.Name == createUserDto.Name);
            return _mapper.Map<UserDataDto>(createUser);
        }

        var errors = string.Join(",",result.Errors.Select(e => e.Description));

        throw new ApplicationException($"No se pudo realizar el registro: {errors}");
    }

    public async Task<UserDataDto?> Update(string id, UserUpdateDto userUpdateDto)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return null;
        }

        user.Name = userUpdateDto.Name;
        user.Phone = userUpdateDto.Phone;
        user.UserName = userUpdateDto.UserName;
        user.Email = userUpdateDto.UserName;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            var errors = string.Join(",", result.Errors.Select(e => e.Description));
            throw new ApplicationException($"No se pudo actualizar el usuario: { errors }");
        }

        if (!string.IsNullOrEmpty(userUpdateDto.Role))
        {
            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.Count > 0)
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
            }
            var roleExists = await _roleManager.RoleExistsAsync(userUpdateDto.Role);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new ApplicationRole { Name = userUpdateDto.Role });
            }
            await _userManager.AddToRoleAsync(user, userUpdateDto.Role);
        }
        return _mapper.Map<UserDataDto>(user);
        }
    /* public async Task<ApplicationUser> Update(User userUpdateDto)
    {
        if(userUpdateDto == null)
        {
            return null!;
        }

        var role = await _db.Roles.FindAsync(userUpdateDto.RoleId);

        var userToUpdate = await _db.Users.FirstOrDefaultAsync(u => u.UserId == userUpdateDto.UserId);
        if (userToUpdate == null)
        {
            return null!;
        }

        userToUpdate.Name = userUpdateDto.Name;
        userToUpdate.Phone = userUpdateDto.Phone;
        userToUpdate.Email = userUpdateDto.Email;
        userToUpdate.RoleId = userUpdateDto.RoleId;
        userToUpdate.Role = role!;

        // Marcar solo los campos que se están actualizando
        var entry = _db.Users.Entry(userToUpdate);
        entry.Property(x => x.Name).IsModified = true;
        entry.Property(x => x.Phone).IsModified = true;
        entry.Property(x => x.Email).IsModified = true;
        entry.Property(x => x.RoleId).IsModified = true;
        entry.Property(x => x.UpdateAt).CurrentValue = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        entry.Property(x => x.UpdateAt).IsModified = true;

        await _db.SaveChangesAsync();

        return new UserResponseDto
        {
            UserId = userToUpdate.UserId,
            Name = userToUpdate.Name,
            Phone = userToUpdate.Phone,
            Email = userToUpdate.Email,
            RoleId = userToUpdate.RoleId,
            RoleName = role?.name ?? string.Empty,
            CreateAt = userToUpdate.CreateAt,
            UpdateAt = userToUpdate.UpdateAt
        };
    } */

    public bool Save()
    {
        return _db.SaveChanges() >= 1;
    }

    public async Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto)
    {
        if (string.IsNullOrEmpty(userLoginDto.Username))
        {
            return new UserLoginResponseDto()
            {
                Token="",
                User = null,
                Message = "El email es requerido"
            };
        }

        var user = await _db.ApplicationUsers
        //.Include(u => u.Role)
        .FirstOrDefaultAsync<ApplicationUser>(u => u.UserName != null && u.UserName.ToLower().Trim() == userLoginDto.Username.ToLower().Trim());

        if(user == null)
        {
            return new UserLoginResponseDto()
            {
                Token="",
                User = null,
                Message = "Username no encontrado"
            };
        }

        if (userLoginDto.Password == null)
        {
            return new UserLoginResponseDto()
            {
                Token="",
                User = null,
                Message = "Password requerido"
            };
        }

        bool isValid = await _userManager.CheckPasswordAsync(user, userLoginDto.Password);
        
        if(!isValid)
        {
            return new UserLoginResponseDto()                                                                                   
            {
              Token ="",
              User = null,
              Message = "Credenciales incorrectas"  
            };
        } 

        /* if(!BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.PasswordHash))
        {
            return new UserLoginResponseDto()
            {
              Token ="",
              User = null,
              Message = "Credenciales incorrectas"  
            };
        } */
        
        
        
        // JWT
        var handleToken = new JwtSecurityTokenHandler();
        if (string.IsNullOrWhiteSpace(secretKey))
        {
            throw new InvalidOperationException("SecretKey no está configurada");
        }
        var roles = await _userManager.GetRolesAsync(user);
        var key = Encoding.UTF8.GetBytes(secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
                {
                new Claim("id", user.Id.ToString()),
                new Claim("userName", user.UserName.ToString() ?? string.Empty),
                new Claim(ClaimTypes.Role, roles.FirstOrDefault() ?? string.Empty),
                }
            ),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = handleToken.CreateToken(tokenDescriptor);
        return new UserLoginResponseDto()
        {
            Token = handleToken.WriteToken(token),
            User = _mapper.Map<UserDataDto>(user),
           /*  User = new UserDto()
            {
                UserId = user.UserId,
                Name = user.Name,
                Phone = user.Phone,
                Email = user.Email,
                RoleId = user.RoleId,
                RoleName = user.Role.name,
                CreateAt = user.CreateAt,
                UpdateAt = user.UpdateAt
            }, */
            Message = "Usuario logueado correctamente"
        };
    }
}