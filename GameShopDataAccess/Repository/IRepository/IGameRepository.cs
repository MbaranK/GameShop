using GameShopModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopDataAccess.Repository.IRepository
{
    public interface IGameRepository : IRepository<Game>
    {
        void Update(Game game);
    }
}
