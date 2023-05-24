using System.ComponentModel.DataAnnotations;

namespace Panta2.Core.Models.User
{
    public abstract class UserUpdateModel
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }
    }
}
