using System.Security.Claims;

namespace MovieShopMVC.Infra;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
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
}