using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineShop.Core.ViewModels
{
    public class AdminBrandViewModel
    {
        [Display(Name = "برند")]
        [MaxLength(100, ErrorMessage = "مقدار {0} نباید بیش تر از {1} کاراکتر باشد")]
        public string Name { get; set; }

        [Display(Name = "عدم نمایش")]
        public bool NotShow { get; set; }

        [Display(Name = "لوگو")]
        public IFormFile Img { get; set; }

        public string ImgName { get; set; }
    }
}
