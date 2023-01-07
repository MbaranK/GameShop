using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopModels.ViewModel
{
    public class CartVM
    {
        public IEnumerable<Cart> CartList { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}
