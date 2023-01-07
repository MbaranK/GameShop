using GameShop.Models;
using GameShopModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopDataAccess.Repository.IRepository
{
    public interface ICartRepository : IRepository<Cart>
    {
        // Ürün sayısını arttırma ve azaltma işlemleri
        int IncreaseCount(Cart cart, int count);
        int DecreaseCount(Cart cart, int count);
    }
}
