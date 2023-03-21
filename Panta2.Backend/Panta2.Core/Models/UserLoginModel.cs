using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panta2.Core.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "You should provide a Id value.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "You should provide a UserName value.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "You should provide a Password value.")]
        public string? Password { get; set; }
    }
}
