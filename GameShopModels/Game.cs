using GameShop.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopModels
{
    public class Game
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="İsim girilmesi zorunludur.")]
        [Display(Name ="İsim")]
        
        public string Name { get; set; }
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
        [Required]
        [Range(10,10000,ErrorMessage ="Fiyat 10 TL ile 10000 TL arasında olmalıdır.")]
        [Display(Name = "Fiyat")]
        public int Price { get; set; }
        [Required]
        [Display(Name = "Stok")]
        [Range(10, 1000,ErrorMessage = "Stok 10 ile 1000 arasında olmalıdır.")]
        
        public int Stock { get; set; }
        [ValidateNever]


        public string ImgUrl { get; set; }

        //Foreign keyler
        [Required(ErrorMessage = "Tür seçimi zorunludur.")]
        [Display(Name="Tür")]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }

        [Required(ErrorMessage = "Stüdyo seçimi zorunludur.")]
        [Display(Name="Geliştirici Stüdyo")]
        public int StudioId { get; set; }
        [ValidateNever]
        public Studio Studio { get; set; }
    }
}
