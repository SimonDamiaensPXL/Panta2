using System.ComponentModel.DataAnnotations;

namespace Panta2.Core.Models.User
{
    public class UserUserNameUpdateModel : UserUpdateModel
    {
        [Required(ErrorMessage = "UserName is required")]
        public string? UserName { get; set;}
    }
}
