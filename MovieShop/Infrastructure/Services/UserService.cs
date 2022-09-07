using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IFavoriteRepository _favoriteRepository;

    public UserService(IPurchaseRepository purchaseRepository, IFavoriteRepository favoriteRepository)
    {
        _purchaseRepository = purchaseRepository;
        _favoriteRepository = favoriteRepository;
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
    public async Task<bool> FavoriteMovie(FavoriteRequestModel model)
    {
        var moviesFavorited = await _favoriteRepository.GetAllFavoritesByUserId(model.UserId);
        var movie = moviesFavorited.SingleOrDefault(m => m.MovieId == model.MovieId);
        if (movie != null)
        {
            return false;
            //throw new Exception("You have favorited this movie");
        }
        
        var dbFavorite = new Favorite
        {
            MovieId = model.MovieId,
            UserId = model.UserId,
        };

        var createdFavorite = await _favoriteRepository.AddFavorite(dbFavorite);
        if (createdFavorite == null)
        {
            return false;
        }

        return true;
    }
}