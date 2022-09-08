using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly MovieShopDbContext _movieShopDbContext;

    public ReviewRepository(MovieShopDbContext movieShopDbContext)
    {
        _movieShopDbContext = movieShopDbContext;
    }

    public async Task<List<Review>> GetAllReviewsByUserId(int userId)
    {
        var reviews = await _movieShopDbContext.Reviews
            .Where(r => r.UserId == userId)
            .ToListAsync();
        return reviews;
    }

    public async Task<List<Review>> GetAllReviewsByMovieId(int movieId)
    {
        var reviews = await _movieShopDbContext.Reviews
            .Where(r => r.MovieId == movieId)
            .ToListAsync();
        return reviews;
    }

    public async Task<Review> AddReview(Review review)
    {
        _movieShopDbContext.Reviews.Add(review);
        await _movieShopDbContext.SaveChangesAsync();
        return review;
    }
    
    public async Task<Review> RemoveReview(Review review)
    {
        _movieShopDbContext.Reviews.Remove(review);
        await _movieShopDbContext.SaveChangesAsync();
        return review;
    }
    
    public async Task<Review> UpdateReview(Review review)
    {
        _movieShopDbContext.Reviews.Update(review);
        await _movieShopDbContext.SaveChangesAsync();
        return review;
    }
}