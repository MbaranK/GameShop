using GameShopData.Data;
using GameShopDataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopDataAccess.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private ApplicationDbContext _context;
        public UnitofWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            Studio = new StudioRepository(_context);
            Game = new GameRepository(_context);
            Cart = new CartRepository(_context);
            ApplicationUser = new ApplicationUserRepository(_context);
            OrderDetail = new OrderDetailRepository(_context);
            OrderHeader = new OrderHeaderRepository(_context);
        }

        public IStudioRepository Studio { get; private set; }

        public ICategoryRepository Category { get; private set; }

        public IGameRepository Game { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public ICartRepository Cart { get; private set; }

        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IOrderDetailRepository OrderDetail { get; private set; }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
