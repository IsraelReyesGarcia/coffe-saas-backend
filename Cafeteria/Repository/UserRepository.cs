using System;
using Cafeteria.Models.Dtos.User;
using Cafeteria.Repository.IRepository;
using Cafeteria.Models;
using Microsoft.EntityFrameworkCore;

namespace Cafeteria.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public UserResponseDto GetUser(int id)
    {
        var result = _db.Users.Include(u => u.Role).FirstOrDefault(u => u.UserId == id);
        
        if (result == null)
        {
            return null;
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
        };
    }

    public ICollection<UserResponseDto> GetUsers()
    {
        return _db.Users
            .Include(u => u.Role)
            .OrderBy(u => u.Name)
            .Select(u => new UserResponseDto
            {
                UserId = u.UserId,
                Name = u.Name,
                Phone = u.Phone,
                Email = u.Email,
                RoleId = u.RoleId,
                RoleName = u.Role.name,
                CreateAt = u.CreateAt,
                UpdateAt = u.UpdateAt
            }).ToList();
    }

    public bool IsUniqueUser(string email)
    {
        return _db.Users.Any(p =>p.Email.ToLower().Trim() == email.ToLower().Trim());
    }

    public Task<LoginResponseDto> Login(UserLoginDto userLoginDto)
    {
        throw new NotImplementedException();
    }

    public async Task<UserResponseDto> Register(CreateUserDto createUserDto)
    {
        var role = await _db.Roles.FindAsync(createUserDto.RoleId);

        var user = new Cafeteria.Models.User
        {
            Name = createUserDto.Name,
            Phone = createUserDto.Phone,
            Email = createUserDto.Email,
            PasswordHash = createUserDto.Password,
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
        };
    }

    public async Task<UserResponseDto> Update(User userUpdateDto)
    {
        var role = await _db.Roles.FindAsync(userUpdateDto.RoleId);
        if(userUpdateDto == null)
        {
            return null;
        }

        var userToUpdate = await _db.Users.FirstOrDefaultAsync(u => u.UserId == userUpdateDto.UserId);
        if (userToUpdate == null)
        {
            return null;
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
    }

    public bool Save()
    {
        return _db.SaveChanges()>= 0 ? true : false; 
    }
}
