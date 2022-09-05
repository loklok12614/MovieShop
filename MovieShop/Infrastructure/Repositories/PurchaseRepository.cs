using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PurchaseRepository : IPurchaseRepository
{
    private readonly MovieShopDbContext _movieShopDbContext;

    public PurchaseRepository(MovieShopDbContext movieShopDbContext)
    {
        _movieShopDbContext = movieShopDbContext;
    }

    public async Task<List<Purchase>> GetAllPurchasesByUserId(int userId)
    {
        var purchases = await _movieShopDbContext.Purchases
            .Include(p => p.Movie)
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.PurchaseDateTime)
            .ToListAsync();
        return purchases;
    }
}