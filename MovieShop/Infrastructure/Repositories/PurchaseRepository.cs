using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
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

    public async Task<PagedResultSet<Purchase>> GetAllPurchasesByUserIdPagination(int userId, int pageSize = 30, int page = 1)
    {
        var totalMoviesCountOfPurchased = await _movieShopDbContext.Purchases.Where(p => p.UserId == userId).CountAsync();
        if (totalMoviesCountOfPurchased == 0)
        {
            var data = new List<Purchase>();
            return new PagedResultSet<Purchase>(data, page, pageSize, totalMoviesCountOfPurchased);
        }

        var purchases = await _movieShopDbContext.Purchases
            .Include(p => p.Movie)
            .Where(p=>p.UserId==userId)
            .OrderByDescending(p => p.PurchaseDateTime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var pagedMoviesPurchased = new PagedResultSet<Purchase>(purchases, page, pageSize, totalMoviesCountOfPurchased);
        return pagedMoviesPurchased;
    }

    public async Task<Purchase> AddPurchase(Purchase purchase)
    {
        _movieShopDbContext.Purchases.Add(purchase);
        await _movieShopDbContext.SaveChangesAsync();
        return purchase;
    }
}