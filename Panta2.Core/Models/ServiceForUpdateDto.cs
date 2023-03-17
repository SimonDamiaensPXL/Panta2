﻿using System.ComponentModel.DataAnnotations;

namespace Panta2.Core.Models
{
    public class ServiceForUpdateDto
    {
        [Required(ErrorMessage = "You should provide a Name value.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "You should provide a Icon value.")]
        public string? Icon { get; set; }
        [Required(ErrorMessage = "You should provide a Link value.")]
        public string? Link { get; set; }
    }
}