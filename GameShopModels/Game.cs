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
        [Required]
        [Display(Name ="İsim")]
        public string Name { get; set; }
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
        [Required]
        [Range(10,10000)]
        [Display(Name = "Fiyat")]
        public int Price { get; set; }
        [Required]
        [Display(Name = "Stok")]
        public int Stock { get; set; }
        [ValidateNever]


        public string ImgUrl { get; set; }

        //Foreign keyler
        [Required]
        [Display(Name="Tür")]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }

        [Required]
        [Display(Name="Geliştirici Stüdyo")]
        public int StudioId { get; set; }
        [ValidateNever]
        public Studio Studio { get; set; }
    }
}
