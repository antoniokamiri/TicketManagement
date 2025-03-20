using Domain.DTO.Requests;
using Domain.DTO.Response;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Common;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;
public class AccountService(SignInManager<User> signInManager) : IAccountService
{
    private readonly SignInManager<User> _signInManager = signInManager;
    public async Task<BaseResponse> RegisterUserAsync(RegisterUserRequest request)
    {
        var user = new User
        {
            Email = request.Email,
            UserName = request.Email,
            AccountConfirmed = false,
        };

        string password = Constants.DEFAULT_PASSWORD;

        var result = await _signInManager.UserManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
            return BaseResponse<string>.Failure(errorMessages);
        }
        else
        {
            return BaseResponse<string>.Success(user.UserName!);
        }
    }

    public async Task<BaseResponse<string>> VerifyUserAsync(string email, string password)
    {
        var user = await _signInManager.UserManager.FindByEmailAsync(email);

        if (user is null)
        {
             return BaseResponse<string>.Failure("User not found");
        }

        var result = await _signInManager.UserManager.CheckPasswordAsync(user, password);

        if(!result)
        {
            return BaseResponse<string>.Failure("User not found");
        }
        else
        {
            return BaseResponse<string>.Success(user.UserName!);
        }
    }
}
