using System.ComponentModel.DataAnnotations;

namespace Panta2.Core.Models.User
{
    public class UserRoleUpdateModel : UserUpdateModel
    {
        //[Required(ErrorMessage = "Role is required")]
        public int RoleId { get; set;}

        //[Required(ErrorMessage = "New Role is required")]
        public int NewRoleId { get; set; }
    }
}
