namespace ApplicationCore.Models;

public class UserDetailsModel
{
    public UserDetailsModel()
    {
        Roles = new List<RoleModel>();
    }

    public int Id { get; set; }

    public DateTime? DateOfBirth { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfilePictureUrl { get; set; }
    
    public List<RoleModel> Roles { get; set; }
}