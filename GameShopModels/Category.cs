using System.ComponentModel.DataAnnotations;

namespace GameShop.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Tür")]
        public string Name { get; set; }
        [Required]
        [Display(Name="Görünme Sırası")]
        public int DisplayOrder { get; set; }
    }
}
