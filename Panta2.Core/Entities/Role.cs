using Microsoft.AspNetCore.Identity;

namespace Panta2.Core.Entities
{
    public class Role : IdentityRole<int>
    {
        public int UserId { get; set; }
    }
}
