using System.ComponentModel.DataAnnotations;

namespace Panta2.Core.Models.Role
{
    public class RoleCreationModel
    {
        [Required(ErrorMessage = "You should provide a Name value.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "You should provide a Services value.")]
        public int[]? Services { get; set; }
    }
}
