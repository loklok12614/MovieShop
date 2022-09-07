using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IUserService
{
    // Purchase
    Task<Guid> PurchaseMovie(PurchaseRequestModel model);
    Task<bool> IsMoviePurchased(int movieId, int userId);
    Task<List<MovieCardPurchasedModel>> GetAllPurchasesByUserId(int userId);

    Task<PagedResultSet<MovieCardPurchasedModel>> GetAllPurchasesByUserIdPagination(int userId, int pageSize = 30,
        int page = 1);
    
    // Favorite
    Task<bool> FavoriteMovie(FavoriteRequestModel model);
}