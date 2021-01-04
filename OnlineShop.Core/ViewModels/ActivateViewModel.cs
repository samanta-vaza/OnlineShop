using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;


namespace OnlineShop.Core.ViewModels
{
    public class ActivateViewModel
    {
        [Display(Name = "کد")]
        [Required(ErrorMessage = "نباید بدون مقدار باشد")]
        [Phone(ErrorMessage = "فقط عدد می توانید وارد کنید")]
        [MaxLength(6, ErrorMessage = "مقدار{0} نباید بیش تر از {1} کاراکتر باشد")]
        [MinLength(6, ErrorMessage = "مقدار{0} نباید کمتر از {1} کاراکتر باشد")]
        public string ActiveCode { get; set; }
    }
}
