using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panta2.Core.Models.Service
{
    public class ServiceWithIsInRoleModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool? IsInRole { get; set; }
    }
}
