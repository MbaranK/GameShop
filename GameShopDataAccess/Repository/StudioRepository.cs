using GameShop.Models;
using GameShopData.Data;
using GameShopDataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopDataAccess.Repository
{
    public class StudioRepository : Repository<Studio>, IStudioRepository
    {
        private ApplicationDbContext _context;
        public StudioRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void update(Studio studio)
        {
            _context.Studios.Update(studio);
        }
    }
}
