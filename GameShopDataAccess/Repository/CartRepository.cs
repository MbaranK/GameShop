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
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public int DecreaseCount(Cart cart, int count)
        {
            cart.Count -= count;
            return cart.Count;
        }

        public int IncreaseCount(Cart cart, int count)
        {
            cart.Count += count;
            return cart.Count;
        }
    }
}
