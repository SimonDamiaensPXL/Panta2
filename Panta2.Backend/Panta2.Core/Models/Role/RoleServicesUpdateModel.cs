using System.ComponentModel.DataAnnotations;

namespace Panta2.Core.Models.Role
{
    public class RoleServicesUpdateModel
    {
        [Required(ErrorMessage = "You should provide a Id value.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "You should provide a Services value.")]
        public int[]? ServiceIds { get; set; }
    }
}
