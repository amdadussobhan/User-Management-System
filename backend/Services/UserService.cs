using backend.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System;
using backend.DTOs;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Google.Protobuf.WellKnownTypes;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services
{
    public class UserService : IUserService
    {
        private readonly UserDbContext _context;

        public UserService(UserDbContext context)
        {
            _context = context;
        }


        public async Task<List<UserListDTO>> GetUsersAsync()
        {
            var users = await _context.Users
                                      .Select(user => new UserListDTO
                                      {
                                          Id = user.Id,
                                          Email = user.Email,
                                          Name = user.Name,
                                          LastLogin = user.LastLogin
                                      })
                                      .ToListAsync();

            return users;
        }


        public async Task<string> RegisterAsync(UserRegisterDTO userRegisterDTO)
        {
            try
            {
                var user = new User
                {
                    Name = userRegisterDTO.Name,
                    Email = userRegisterDTO.Email,
                    PasswordHash = HashPassword(userRegisterDTO.Password)
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return "User registered successfully.";
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_Users_Email"))
                    return "Email is already in use.";

                throw;
            }
        }


        public async Task<string> LoginUserAsync(UserLoginDTO userLoginDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userLoginDTO.Email);
            if (user == null || !VerifyPassword(userLoginDTO.Password, user.PasswordHash))            
                return "Invalid credentials.";
            
            if (user.Status == "blocked") return "User account is blocked.";

            user.LastLogin = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return "Login Successful.";
        }


        public async Task<string> UpdateUserAsync(int id, UserUpdateDTO userUpdateDTO)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null) return "User not found.";

                user.Name = userUpdateDTO.Name ?? user.Name;
                user.Email = userUpdateDTO.Email ?? user.Email;

                if (!string.IsNullOrWhiteSpace(userUpdateDTO.Password))
                    user.PasswordHash = HashPassword(userUpdateDTO.Password);

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return "User updated Successfully.";
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_Users_Email"))
                    return "Email is already in use.";

                throw;
            }
        }


        public async Task<string> UpdateUserStatusAsync(UserStatusUpdateDTO userUpdateDTO)
        {
            var user = await _context.Users.FindAsync(userUpdateDTO.UserId);
            if (user == null) return "User not found.";

            user.Status = userUpdateDTO.Status;
            await _context.SaveChangesAsync();

            return "User updated Successfully.";
        }


        public async Task<string> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return "User not found.";
            
            user.IsDeleted = true;
            await _context.SaveChangesAsync();

            return "User deleted Successfully.";
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            var hash = HashPassword(password);
            return hash == storedHash;
        }
    }
}
