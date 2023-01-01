using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameShop.Models
{
    public class Studio
    {

        public int Id { get; set; }
        [Required]
        [DisplayName("Geliştirici Stüdyo")]
        public string Name { get; set; }
    }
}
