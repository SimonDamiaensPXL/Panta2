using Microsoft.AspNetCore.Identity;

namespace Panta2.Core.Entities
{
    public class User : IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int CompanyId { get; set; }
    }
}
