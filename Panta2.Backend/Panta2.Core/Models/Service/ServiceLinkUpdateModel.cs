using System.ComponentModel.DataAnnotations;

namespace Panta2.Core.Models.Service
{
    public class ServiceLinkUpdateModel
    {
        [Required(ErrorMessage = "You should provide a Id value.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "You should provide a Link value.")]
        public string? Link { get; set; }
    }
}
