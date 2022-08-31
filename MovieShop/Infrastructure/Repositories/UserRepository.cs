using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MovieShopDbContext _movieShopDbContext;

    public UserRepository(MovieShopDbContext movieShopDbContext)
    {
        _movieShopDbContext = movieShopDbContext;
    }
    public async Task<User> GetUserByEmail(string email)
    {
        var user = await _movieShopDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }

    public async Task<User> AddUser(User user)
    {
        _movieShopDbContext.Users.Add(user);
        await _movieShopDbContext.SaveChangesAsync();
        return user;
    }
}