namespace Panta2.Core.Models.Company
{
    public class CompanyServiceModel
    {
        public int CompanyId { get; set; }
        public int ServiceId { get; set; }
        public string? Name { get; set; }
        public string? Icon { get; set; }
        public bool Enabled { get; set; }
        public int Order { get; set; }
    }
}
