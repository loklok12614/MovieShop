using System.Security.Claims;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace MovieShopMVC.Infra;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserService _userService;

    public CurrentUser(IHttpContextAccessor httpContextAccessor, IUserService userService)
    {
        _httpContextAccessor = httpContextAccessor;
        _userService = userService;
    }

    public int UserId => Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

    public string FullName =>
        _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.GivenName)
        + " " +
        _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Surname);

    public string LastName => _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c=> c.Type == ClaimTypes.Surname).Value;

    public string FirstName => _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c=> c.Type == ClaimTypes.GivenName).Value;

    public bool IsAdmin => throw new NotImplementedException();

    public bool IsAuthenticated => _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

    public string Email => _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)
        .Value;

    public string ProfilePictureUrl => throw new NotImplementedException();
    public async Task<bool> IsMoviePurchased(int movieId)
    {
        return await _userService.IsMoviePurchased(movieId, UserId);
    }

    public async Task<bool> IsMovieFavorited(int movieId)
    {
        return await _userService.IsMovieFavorited(movieId, UserId);
    }

    public async Task<ReviewRequestModel> GetUserReviewForMovie(int movieId)
    {
        return await _userService.GetReviewByUserIdAndMovieId(UserId, movieId);
    }
}