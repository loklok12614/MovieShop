using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repositories;

public interface IPurchaseRepository
{
    Task<List<Purchase>> GetAllPurchasesByUserId(int userId);
    Task<PagedResultSet<Purchase>> GetAllPurchasesByUserIdPagination(int userId, int pageSize = 30, int page = 1);
    Task<Purchase> AddPurchase(Purchase purchase);
}