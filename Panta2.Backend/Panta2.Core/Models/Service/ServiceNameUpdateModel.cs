using System.ComponentModel.DataAnnotations;

namespace Panta2.Core.Models.Service
{
    public class ServiceNameUpdateModel
    {
        [Required(ErrorMessage = "You should provide a Id value.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "You should provide a Name value.")]
        public string? Name { get; set; }
    }
}
