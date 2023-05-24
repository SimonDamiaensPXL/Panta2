using System.ComponentModel.DataAnnotations;

namespace Panta2.Core.Models.User
{
    public class UserPasswordUpdateModel : UserUpdateModel
    {
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}
