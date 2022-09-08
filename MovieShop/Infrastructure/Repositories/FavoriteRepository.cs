using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FavoriteRepository : IFavoriteRepository
{
    private readonly MovieShopDbContext _movieShopDbContext;

    public FavoriteRepository(MovieShopDbContext movieShopDbContext)
    {
        _movieShopDbContext = movieShopDbContext;
    }

    public async Task<List<Favorite>> GetAllFavoritesByUserId(int userId)
    {
        var favorites = await _movieShopDbContext.Favorites
            .Include(f => f.Movie)
            .Where(f => f.UserId == userId)
            .ToListAsync();
        return favorites;
    }

    public async Task<PagedResultSet<Favorite>> GetAllFavoritesByUserIdPagination(int userId, int pageSize = 30, int page = 1)
    {
        var totalMoviesCountOfFavorited = await _movieShopDbContext.Favorites.Where(f => f.UserId == userId).CountAsync();
        if (totalMoviesCountOfFavorited == 0)
        {
            var data = new List<Favorite>();
            return new PagedResultSet<Favorite>(data, page, pageSize, totalMoviesCountOfFavorited);
        }

        var favorites = await _movieShopDbContext.Favorites
            .Include(f => f.Movie)
            .Where(f => f.UserId == userId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize).ToListAsync();
        
        return new PagedResultSet<Favorite>(favorites, page, pageSize, totalMoviesCountOfFavorited);
    }

    public async Task<Favorite> AddFavorite(Favorite favorite)
    {
        _movieShopDbContext.Favorites.Add(favorite);
        await _movieShopDbContext.SaveChangesAsync();
        return favorite;
    }

    public async Task<Favorite> RemoveFavorite(Favorite favorite)
    {
        _movieShopDbContext.Favorites.Remove(favorite);
        await _movieShopDbContext.SaveChangesAsync();
        return favorite;
    }
}