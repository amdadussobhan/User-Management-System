using backend.DTOs;
using backend.Entities;

namespace backend.Services
{
    public interface IUserService
    {
        Task<List<UserListDTO>> GetUsersAsync();
        Task<string> RegisterAsync(UserRegisterDTO userRegisterDTO);
        Task<string> LoginUserAsync(UserLoginDTO userLoginDTO);
        Task<string> UpdateUserAsync(int id, UserUpdateDTO userUpdateDTO);
        Task<string> UpdateUserStatusAsync(UserStatusUpdateDTO userUpdateDTO);
        Task<string> DeleteUserAsync(int id);
    }
}
