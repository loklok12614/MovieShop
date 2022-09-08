using System.Net;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IFavoriteRepository _favoriteRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IMovieRepository _movieRepository;

    public UserService(IPurchaseRepository purchaseRepository, IFavoriteRepository favoriteRepository, IReviewRepository reviewRepository, IMovieRepository movieRepository)
    {
        _purchaseRepository = purchaseRepository;
        _favoriteRepository = favoriteRepository;
        _reviewRepository = reviewRepository;
        _movieRepository = movieRepository;
    }

    // Purchase
    public async Task<Guid> PurchaseMovie(PurchaseRequestModel model)
    {
        // check if movie is purchased
        var moviesPurchased = await _purchaseRepository.GetAllPurchasesByUserId(model.UserId);
        var movie = moviesPurchased.SingleOrDefault(m => m.MovieId == model.MovieId);
        if (movie != null)
        {
            return Guid.Empty;
        }
        
        var dbPurchase = new Purchase
        {
            MovieId = model.MovieId,
            UserId = model.UserId,
            TotalPrice = model.TotalPrice,
            PurchaseNumber = model.PurchaseNumber
        };

        var createdPurchase = await _purchaseRepository.AddPurchase(dbPurchase);

        return createdPurchase.PurchaseNumber;
    }

    public async Task<bool> IsMoviePurchased(int movieId, int userId)
    {
        var moviesPurchased = await _purchaseRepository.GetAllPurchasesByUserId(userId);
        var movie = moviesPurchased.SingleOrDefault(m => m.MovieId == movieId);
        if (movie == null)
        {
            return false;
        }

        return true;
    }

    public async Task<List<MovieCardPurchasedModel>> GetAllPurchasesByUserId(int userId)
    {
        var moviesPurchased = await _purchaseRepository.GetAllPurchasesByUserId(userId);
        var movieCards = new List<MovieCardPurchasedModel>();
        
        movieCards.AddRange(moviesPurchased.Select(p=> new MovieCardPurchasedModel
        {
            Id = p.MovieId,
            PosterUrl = p.Movie.PosterUrl,
            Title = p.Movie.Title,
            PurchaseDateTime = p.PurchaseDateTime,
            PurchaseNumber = p.PurchaseNumber,
            TotalPrice = p.TotalPrice
        }));

        return movieCards;
    }

    public async Task<PagedResultSet<MovieCardPurchasedModel>> GetAllPurchasesByUserIdPagination(int userId, int pageSize = 30, int page = 1)
    {
        var moviesPurchased = await _purchaseRepository.GetAllPurchasesByUserIdPagination(userId, pageSize, page);
        var movieCardsPurchased = new List<MovieCardPurchasedModel>();
        
        movieCardsPurchased.AddRange(moviesPurchased.Data.Select(p=>new MovieCardPurchasedModel
        {
            Id = p.MovieId,
            PosterUrl = p.Movie.PosterUrl,
            Title = p.Movie.Title,
            PurchaseDateTime = p.PurchaseDateTime,
            PurchaseNumber = p.PurchaseNumber,
            TotalPrice = p.TotalPrice
        }));
        return new PagedResultSet<MovieCardPurchasedModel>(movieCardsPurchased, page, pageSize,
            moviesPurchased.TotalRowCount);
    }

    // Favorite
    public async Task<int> FavoriteMovie(FavoriteRequestModel model)
    {
        var moviesFavorited = await _favoriteRepository.GetAllFavoritesByUserId(model.UserId);
        var movie = moviesFavorited.SingleOrDefault(m => m.MovieId == model.MovieId);
        if (movie != null)
        {
            return model.MovieId;
            //throw new Exception("You have favorited this movie");
        }
        
        var dbFavorite = new Favorite
        {
            MovieId = model.MovieId,
            UserId = model.UserId,
        };

        var createdFavorite = await _favoriteRepository.AddFavorite(dbFavorite);
        return createdFavorite.MovieId;
    }

    public async Task<int> RemoveFavoriteMovie(FavoriteRequestModel model)
    {
        var moviesFavorited = await _favoriteRepository.GetAllFavoritesByUserId(model.UserId);
        var favoriteToRemove = moviesFavorited.SingleOrDefault(m => m.MovieId == model.MovieId);
        if (favoriteToRemove == null)
        {
            return model.MovieId;
        }

        var removedFavorite = await _favoriteRepository.RemoveFavorite(favoriteToRemove);
        return removedFavorite.MovieId;
    }
    public async Task<bool> IsMovieFavorited(int movieId, int userId)
    {
        var moviesFavorited = await _favoriteRepository.GetAllFavoritesByUserId(userId);
        var movie = moviesFavorited.SingleOrDefault(m => m.MovieId == movieId);
        return movie != null && true;
    }

    public async Task<PagedResultSet<MovieCardModel>> GetAllFavoritesByUserPagination(int userId, int pageSize = 30, int page = 1)
    {
        var moviesFavorited = await _favoriteRepository.GetAllFavoritesByUserIdPagination(userId, pageSize, page);
        var movieCardsFavorited = new List<MovieCardModel>();
        
        movieCardsFavorited.AddRange(moviesFavorited.Data.Select(f => new MovieCardModel
        {
            Id = f.MovieId,
            PosterUrl = f.Movie.PosterUrl,
            Title = f.Movie.Title
        }));

        return new PagedResultSet<MovieCardModel>(movieCardsFavorited, page, pageSize, moviesFavorited.TotalRowCount);
    }

    
    // Review
    public async Task<int> ReviewMovie(ReviewRequestModel model)
    {
        var moviesReviewd = await _reviewRepository.GetAllReviewsByUserId(model.UserId);
        var movie = moviesReviewd.SingleOrDefault(m => m.MovieId == model.MovieId);
        if (movie != null)
        {
            return model.MovieId;
        }

        var dbReview = new Review
        {
            MovieId = model.MovieId,
            UserId = model.UserId,
            Rating = model.Rating,
            ReviewText = model.ReviewText
        };

        var createdReview = await _reviewRepository.AddReview(dbReview);
        
        return createdReview.MovieId;
    }

    public async Task<int> DeleteReview(int movieId, int userId)
    {
        var reviews = await _reviewRepository.GetAllReviewsByUserId(userId);
        var reviewToDelete = reviews.SingleOrDefault(r => r.MovieId == movieId);
        if (reviewToDelete == null)
        {
            return movieId; //no review found
        }

        var deletedReview = await _reviewRepository.RemoveReview(reviewToDelete);
        return deletedReview.MovieId;
    }

    public async Task<int> EditReview(ReviewRequestModel model)
    {
        var reviews = await _reviewRepository.GetAllReviewsByUserId(model.UserId);
        var reviewToUpdate = reviews.SingleOrDefault(r => r.MovieId == model.MovieId);
        if (reviewToUpdate == null)
        {
            return model.MovieId; //no review found
        }

        reviewToUpdate.Rating = model.Rating;
        reviewToUpdate.ReviewText = model.ReviewText;
        var updatedReview = await _reviewRepository.UpdateReview(reviewToUpdate);
        return updatedReview.MovieId;
    }

    public async Task<ReviewRequestModel> GetReviewByUserIdAndMovieId(int userId, int movieId)
    {
        var allReviewsByUser = await _reviewRepository.GetAllReviewsByUserId(userId);
        var userReviewOnMovie = allReviewsByUser.SingleOrDefault(r => r.MovieId == movieId);
        if (userReviewOnMovie == null)
        {
            // If User never reviewed this movie, return a model with empty value
            return new ReviewRequestModel
            {
                MovieId = 0, UserId = 0, Rating = 0, ReviewText = ""
            };
        }

        var model = new ReviewRequestModel
        {
            MovieId = userReviewOnMovie.MovieId,
            UserId = userReviewOnMovie.UserId,
            Rating = userReviewOnMovie.Rating,
            ReviewText = userReviewOnMovie.ReviewText
        };
        return model;
    }
}