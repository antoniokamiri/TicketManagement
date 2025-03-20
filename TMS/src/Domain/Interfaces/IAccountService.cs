using Domain.DTO.Requests;
using Domain.DTO.Response;

namespace Domain.Interfaces;
public interface IAccountService
{
    Task<BaseResponse<string>> VerifyUserAsync(string email, string password);
    Task<BaseResponse> RegisterUserAsync(RegisterUserRequest request);
}
