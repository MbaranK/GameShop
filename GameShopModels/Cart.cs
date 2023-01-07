using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopModels
{
    public class Cart
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        [ValidateNever]
        [ForeignKey("GameId")]
        public Game Game { get; set; }
        [Range(1, 10, ErrorMessage = "1 ila 10 arasında ekleme yapabilirsiniz.")]
        public int Count { get; set; }
        public string ApplicationUserId { get; set; }
        [ValidateNever]
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        //database 'e eklenmesini istemiyorum.
        [NotMapped]
        public double Price { get; set; }
    }
}
