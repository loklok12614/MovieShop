using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models;

public class UserLoginModel
{
    [Required(ErrorMessage = "Email should not be empty")]
    [EmailAddress(ErrorMessage = "Email should be in right format")]
    [StringLength(80, ErrorMessage = "Email cannot exceed 80 character")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password should not be empty")]
    public string Password { get; set; }
}