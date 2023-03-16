namespace Panta2.Core.Entities
{
    public class CompanyService
    {
        public int CompanyId { get; set; }
        public int ServiceId { get; set; }

        public string Name { get; set; }
        public string IconName { get; set; }
        public bool Enabled { get; set; }
        public int Order { get; set; }
    }
}
