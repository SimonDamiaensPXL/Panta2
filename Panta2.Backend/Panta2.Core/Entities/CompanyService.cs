using System.ComponentModel.DataAnnotations;

namespace Panta2.Core.Entities
{
    public class CompanyService
    {
        [Key]
        public int CompanyId { get; set; }
        [Key]
        public int ServiceId { get; set; }

        public string? Name { get; set; }
        public string? Icon { get; set; }
        public bool Enabled { get; set; }
        public int Order { get; set; }
    }
}
