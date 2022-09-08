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
    Task<int> FavoriteMovie(FavoriteRequestModel model);
    Task<int> RemoveFavoriteMovie(FavoriteRequestModel model);
    Task<bool> IsMovieFavorited(int movieId, int userId);
    Task<PagedResultSet<MovieCardModel>> GetAllFavoritesByUserPagination(int userId, int pageSize = 30, int page = 1);
    
    // Review
    Task<int> ReviewMovie(ReviewRequestModel model);
    Task<int> DeleteReview(int movieId, int userId);
    Task<int> EditReview(ReviewRequestModel model);
    Task<ReviewRequestModel> GetReviewByUserIdAndMovieId(int userId, int movieId);
}