using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopModels
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name ="İsim")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Soyisim")]
        public string Surname { get; set; }
        [Display(Name = "Adres")]
        public string Address { get; set; }
        [Display(Name = "İlçe")]
        public string State { get; set; }
        [Display(Name = "Şehir")]
        public string City { get; set; }

    }
}
