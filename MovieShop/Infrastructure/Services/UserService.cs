using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IPurchaseRepository _purchaseRepository;

    public UserService(IPurchaseRepository purchaseRepository)
    {
        _purchaseRepository = purchaseRepository;
    }

    public async Task<bool> PurchaseMovie(PurchaseRequestModel model)
    {
        // check if movie is purchased
        var moviesPurchased = await _purchaseRepository.GetAllPurchasesByUserId(model.UserId);
        var movie = moviesPurchased.SingleOrDefault(m => m.MovieId == model.MovieId);
        if (movie != null)
        {
            throw new Exception("You have owned this movie");
        }
        
        var dbPurchase = new Purchase
        {
            MovieId = model.MovieId,
            UserId = model.UserId,
            TotalPrice = model.TotalPrice,
            PurchaseNumber = model.PurchaseNumber
        };

        var createdPurchase = await _purchaseRepository.AddPurchase(dbPurchase);
        return true;
    }

    public async Task<bool> IsMoviePurchased(PurchaseRequestModel model)
    {
        var moviesPurchased = await _purchaseRepository.GetAllPurchasesByUserId(model.UserId);
        var movie = moviesPurchased.SingleOrDefault(m => m.MovieId == model.MovieId);
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
}