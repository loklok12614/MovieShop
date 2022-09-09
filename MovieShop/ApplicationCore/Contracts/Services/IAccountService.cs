using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IAccountService
{
    Task<UserLoginSuccessModel> ValidateUser(UserLoginModel model);
    Task<bool> CheckEmail(string email);
    Task<int> RegisterUser(UserRegisterModel model);
}