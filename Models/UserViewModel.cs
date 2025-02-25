using System.ComponentModel.DataAnnotations;

namespace pizzashop.Models;

public class UserViewModel
{
    public int Id { get; set; }

    public string? Email { get; set; }

    public int RoleId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Phone number is required")]
    public string Phone { get; set; }

    public string? ProfileImage { get; set; }

    public string? Password { get; set; }

    public int? CountryId { get; set; }

    public int? StateId { get; set; }

    public int? CityId { get; set; }

    public string? Zipcode { get; set; }

    public string? Address { get; set; }

    public string? RoleName { get; set; }

    public string? CountryName { get; set; }

    public string? StateName { get; set; }

    public string? CityName { get; set; }

    public bool? IsDeleted { get; set; }

    public bool? IsActive { get; set; }

}
