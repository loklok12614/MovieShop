namespace MovieShopMVC.Infra;

public interface ICurrentUser
{
    public int UserId { get; }
    string FullName { get; }
    string LastName { get; }
    string FirstName { get; }

    bool IsAdmin { get; }
    bool IsAuthenticated { get; }
    string Email { get; }
    string ProfilePictureUrl { get; }
    Task<bool> IsMoviePurchased(int movieId);
}