using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IUserRepository
{
    Task<User> GetUserByEmail(string email);
    Task<User> GetUserDetailsById(int userId);

    Task<User> AddUser(User user);
}