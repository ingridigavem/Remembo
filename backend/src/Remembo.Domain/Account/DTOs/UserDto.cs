using System.ComponentModel.DataAnnotations;

namespace Remembo.Domain.Account.DTOs;
public class UserDto {
    [Required(ErrorMessage = "Name required")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Email required")]
    [EmailAddress(ErrorMessage = "Email invalid")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password required")]
    public string Password { get; set; } = null!;
}
