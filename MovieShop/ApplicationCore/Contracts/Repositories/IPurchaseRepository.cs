using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IPurchaseRepository
{
    Task<List<Purchase>> GetAllPurchasesByUserId(int userId);
}