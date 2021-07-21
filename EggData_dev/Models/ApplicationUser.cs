using System;
using Microsoft.AspNetCore.Identity;

namespace EggData_dev.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string CustomTag { get; set; }
    }
}
