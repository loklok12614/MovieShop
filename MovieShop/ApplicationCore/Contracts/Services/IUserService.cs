using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IUserService
{
    Task<bool> PurchaseMovie(PurchaseRequestModel model);
    Task<bool> IsMoviePurchased(int movieId, int userId);
    Task<List<MovieCardPurchasedModel>> GetAllPurchasesByUserId(int userId);

    Task<PagedResultSet<MovieCardPurchasedModel>> GetAllPurchasesByUserIdPagination(int userId, int pageSize = 30,
        int page = 1);
}