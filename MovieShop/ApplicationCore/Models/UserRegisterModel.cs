using System.ComponentModel.DataAnnotations;
using ApplicationCore.Validatiors;

namespace ApplicationCore.Models;

public class UserRegisterModel
{
    [Required(ErrorMessage = "Email should not be empty")]
    [EmailAddress(ErrorMessage = "Email should be in right format")]
    [StringLength(80, ErrorMessage = "Email cannot exceed 80 character")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password should not be empty")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = 
        "Password Should have minimum of 8 characters with at least one upper, lower, number and special character")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "First name should not be empty")]
    [StringLength(80, ErrorMessage = "First name cannot exceed 80 character")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Last name should not be empty")]
    [StringLength(80, ErrorMessage = "Last name cannot exceed 80 character")]
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "Date of birth should not be empty")]
    [StringLength(80, ErrorMessage = "Last name cannot exceed 80 character")]
    [MinimumAllowedYear]
    public DateTime DateOfBirth { get; set; }
}