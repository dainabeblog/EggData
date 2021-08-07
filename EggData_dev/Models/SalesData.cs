using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EggData_dev.Controllers;
using Microsoft.AspNetCore.Identity;

namespace EggData_dev.Models
{
    public class SalesData
    {
        public int SalesDataId { get; set; }
        [Required]
        public DateTime date { get; set; }

        [Required]
        public int saledCount { get; set; }

        [Display(Name = "商品名")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Display(Name = "店舗名")]
        public int StoreId { get; set; }
        public Store Store { get; set; }

        [ProtectedPersonalData]
        public virtual string UserName { get; set; }


        //public SalesData()
        //{
        //    saledCount = 0;
        //}
        //public SalesData(int SaledCount)
        //{
        //    saledCount = SaledCount;
        //}
        //public override string ToString()
        //{
        //    return "{ " + saledCount + ", }";
        //}


        public static implicit operator SalesData(List<SalesData> v)
        {
            throw new NotImplementedException();
        }
    }
}
