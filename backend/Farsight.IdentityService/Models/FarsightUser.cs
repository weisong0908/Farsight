using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Farsight.IdentityService.Models
{
    public class FarsightUser : IdentityUser
    {
        [PersonalData]
        public byte[] ProfilePicture { get; set; }
        [PersonalData]
        public string DisplayName { get; set; }
    }
}