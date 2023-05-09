using System.ComponentModel.DataAnnotations;

namespace Panta2.Core.Models.Company
{
    public class CompanyCreationModel
    {
        [Required(ErrorMessage = "You should provide a Name value.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "You should provide a Logo value.")]
        public string? Logo { get; set; }

    }
}
