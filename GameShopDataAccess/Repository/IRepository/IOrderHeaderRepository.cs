using GameShop.Models;
using GameShopModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopDataAccess.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        // Ürün sayısını arttırma ve azaltma işlemleri
        void Update(OrderHeader obj);
    }
}
