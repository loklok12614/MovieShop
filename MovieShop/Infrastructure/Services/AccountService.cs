using System.Security.Cryptography;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;

using Microsoft.AspNetCore.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Infrastructure.Services;

public class AccountService : IAccountService
{
    private readonly IUserRepository _userRepository;

    public AccountService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<UserLoginSuccessModel> ValidateUser(UserLoginModel model)
    {
        var user = await _userRepository.GetUserByEmail(model.Email);
        if (user == null)
        {
            return null;
            // throw new Exception("Incorrect credentials");
        }

        var hashedPassword = GetHashedPassword(model.Password, user.Salt);
        if (user.HashedPassword != hashedPassword)
        {
            return null;
            // throw new Exception("Incorrect credentials");
        }

        return new UserLoginSuccessModel
        {
            Id = user.Id, Email = user.Email, FirstName = user.FirstName, LastName = user.LastName
        };
    }

    public async Task<bool> CheckEmail(string email)
    {
        var emailInDb = await _userRepository.GetUserByEmail(email);
        return emailInDb != null && true; //return true if email already exist
    }

    public async Task<int> RegisterUser(UserRegisterModel model)
    {
        // check if email exists in database
        // go to user table using repository
        var user = await _userRepository.GetUserByEmail(model.Email);
        if (user != null)
        {
            return 0;
            // throw new Exception("Email already exists, try to login");
        }
        
        // create a random salt
        // hash the password with salt created in above step
        // create new user entity object and save it to db using EF core SaveChanges method
        var salt = GetRandomSalt();

        var hashedPassword = GetHashedPassword(model.Password, salt);

        var dbUser = new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Salt = salt,
            HashedPassword = hashedPassword,
            DateOfBirth = model.DateOfBirth
        };

        var createdUser = await _userRepository.AddUser(dbUser);
        return createdUser.Id;
    }

    private string GetRandomSalt()
    {
        var randomBytes = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        return Convert.ToBase64String(randomBytes);
    }

    private string GetHashedPassword(string password, string salt)
    {
        var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password,
            Convert.FromBase64String(salt),
            KeyDerivationPrf.HMACSHA512,
            10000,
            256 / 8));
        return hashed;
    }
}