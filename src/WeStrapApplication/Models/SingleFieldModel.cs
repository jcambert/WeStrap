﻿using System.ComponentModel.DataAnnotations;
using WeStrap;

namespace WeStrapApplication.Models
{
    public class SingleFieldModel
    {
        [StringLength(20, MinimumLength = 5, ErrorMessageResourceName = "MIN_LENGTH", ErrorMessageResourceType = typeof(Resources.Models_LoginModel))]
        [Display(Name ="DESCRIPTION")]
        [UpperCase]
        [Required]
        public string Description { get; set; }
    }
}
