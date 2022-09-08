using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IReviewRepository
{
    Task<List<Review>> GetAllReviewsByUserId(int userId);
    Task<List<Review>> GetAllReviewsByMovieId(int movieId);
    Task<Review> AddReview(Review review);
    Task<Review> RemoveReview(Review review);
    Task<Review> UpdateReview(Review review);
}