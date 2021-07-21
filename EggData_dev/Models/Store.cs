﻿using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EggData_dev.Models
{
    public class Store
    {
        public int StoreId { get; set; }
        [Display(Name = "店舗名")]
        public string Name { get; set; }

        [ProtectedPersonalData]
        public virtual string UserName { get; set; }
    }
}
