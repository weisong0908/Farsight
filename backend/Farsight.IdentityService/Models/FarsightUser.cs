using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Farsight.IdentityService.Models
{
    public class FarsightUser : IdentityUser
    {
        [PersonalData]
        public byte[] ProfilePicture { get; set; }
        [PersonalData]
        public string DisplayName { get; set; }
        [PersonalData]
        [StringLength(255)]
        public string StatusMessage { get; set; }
    }
}