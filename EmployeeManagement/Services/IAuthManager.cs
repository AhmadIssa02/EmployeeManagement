using EmployeeManagement.Models;
using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.Services
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserDto userDTO);
        Task<string> CreateToken();
        Task<string> CreateRefreshToken();
        Task<TokenRequest> VerifyRefreshToken(TokenRequest request);
    }
}
