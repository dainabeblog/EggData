using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EggData_dev.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Display(Name = "商品名")]
        public string Name { get; set; }

        [ProtectedPersonalData]
        public virtual string UserName { get; set; }

        [Display(Name = "販売履歴")]
        public ICollection<SalesData> SalesData { get; set; }
    }


}
