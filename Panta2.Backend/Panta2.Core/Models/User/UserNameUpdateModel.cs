using System.ComponentModel.DataAnnotations;

namespace Panta2.Core.Models.User
{
    public class UserNameUpdateModel : UserUpdateModel
    {
        [Required(ErrorMessage = "FirstName is required")]
        public string? FirstName { get; set;}

        [Required(ErrorMessage = "LastName is required")]
        public string? LastName { get; set; }
    }
}
