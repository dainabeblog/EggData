using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EggData_dev.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Display(Name="商品名")]
        public string Name { get; set; }

        //public string IdentityUserId { get; set; }
        //public IdentityUser IdentityUser { get; set; }

        [ProtectedPersonalData]
        public virtual string UserName { get; set; }
    }
}
