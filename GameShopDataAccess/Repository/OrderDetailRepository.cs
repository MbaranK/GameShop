using GameShop.Models;
using GameShopData.Data;
using GameShopDataAccess.Repository.IRepository;
using GameShopModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopDataAccess.Repository
{
    public class OrderDetailRepository : Repository<OrderDetails>, IOrderDetailRepository
    {
        private ApplicationDbContext _context;

        public OrderDetailRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(OrderDetails obj)
        {
            _context.OrderDetails.Update(obj);
        }
    }
}
