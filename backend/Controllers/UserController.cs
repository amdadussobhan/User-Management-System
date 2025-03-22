using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("list")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO userRegisterDTO)
        {
            var result = await _userService.RegisterAsync(userRegisterDTO);
            return result == "User registered successfully." ? Ok(new { message = result }) : BadRequest(new { message = result });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
        {
            var result = await _userService.LoginUserAsync(userLoginDTO);
            return result == "Login Successful." ? Ok(new { message = result }) : Unauthorized(new { message = result });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateDTO userUpdateDTO)
        {
            var result = await _userService.UpdateUserAsync(id, userUpdateDTO);
            return result.Contains("User updated Successfully.") ? Ok(new { message = result }) : BadRequest(new { message = result });            
        }


        [HttpPost("update_status")]
        public async Task<IActionResult> UpdateUserStatus(UserStatusUpdateDTO updateUserStatusDTO)
        {
            var result = await _userService.UpdateUserStatusAsync(updateUserStatusDTO);
            return result.Contains("User updated Successfully.") ? Ok(new { message = result }) : NotFound(new { message = result });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            return result == "User deleted Successfully." ? Ok(new { message = result }) : NotFound(new { message = result });
        }
    }
}
