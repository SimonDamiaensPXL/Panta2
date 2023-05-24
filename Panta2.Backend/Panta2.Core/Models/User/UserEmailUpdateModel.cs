using System.ComponentModel.DataAnnotations;

namespace Panta2.Core.Models.User
{
    public class UserEmailUpdateModel : UserUpdateModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set;}
    }
}
