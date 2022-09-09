using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repositories;

public interface IReviewRepository
{
    Task<List<Review>> GetAllReviewsByUserId(int userId);
    Task<PagedResultSet<Review>> GetAllReviewsByMovieIdPagination(int movieId, int pageSize = 30, int page = 1);
    Task<Review> AddReview(Review review);
    Task<Review> RemoveReview(Review review);
    Task<Review> UpdateReview(Review review);
}