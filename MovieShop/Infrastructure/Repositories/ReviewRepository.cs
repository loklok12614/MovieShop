using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
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

    public async Task<PagedResultSet<Review>> GetAllReviewsByMovieIdPagination(int movieId, int pageSize = 30, int page = 1)
    {
        var totalReviewsCount = await _movieShopDbContext.Reviews.Where(r => r.MovieId == movieId).CountAsync();
        if (totalReviewsCount == 0)
        {
            var data = new List<Review>();
            return new PagedResultSet<Review>(data, page, pageSize, totalReviewsCount);
        }

        var reviews = await _movieShopDbContext.Reviews
            .Include(r => r.Movie)
            .Where(r => r.MovieId == movieId)
            .OrderByDescending(r=> r.CreatedDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return new PagedResultSet<Review>(reviews, page, pageSize, totalReviewsCount);
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
        // _movieShopDbContext.Reviews.Update(review);
        _movieShopDbContext.Entry(review).State = EntityState.Modified;
        await _movieShopDbContext.SaveChangesAsync();
        return review;
    }
}