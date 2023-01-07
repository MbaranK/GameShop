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
	public class OrderDetails
	{
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        [ValidateNever]

        public OrderHeader OrderHeader { get; set; }

        [Required]
        public int GameId { get; set; }
        [ForeignKey("GameId")]
        [ValidateNever]
        public Game Game { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
    }
}
