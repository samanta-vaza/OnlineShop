﻿using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Core.ViewModels
{
    public class SubCategoriesViewModel
    {
        [Display(Name = "نام زیر دسته")]
        public string Name { get; set; }
    }
}
