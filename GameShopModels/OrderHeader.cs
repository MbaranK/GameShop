using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopModels
{
	public class OrderHeader
	{
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public double OrderTotal { get; set; }

        [Required(ErrorMessage ="Lütfen Telefon numaranızı giriniz.")]
        [Display(Name="Telefon Numarası")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Lütfen adres bilginizi giriniz.")]
        [Display(Name = "Adres")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Lütfen şehir bilginizi giriniz.")]
        [Display(Name = "Şehir")]
        public string City { get; set; }
        [Required(ErrorMessage = "Lütfen ilçe bilginizi giriniz.")]
        [Display(Name = "İlçe")]
        public string State { get; set; }
        [Required(ErrorMessage = "Lütfen isim bilginizi giriniz.")]
        [Display(Name = "İsim")]
        public string Name { get; set; }
    }
}
