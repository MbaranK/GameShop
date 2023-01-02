using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopDataAccess.Repository.IRepository
{
    public interface IUnitofWork
    {
        IStudioRepository Studio { get; }

        ICategoryRepository Category { get; }

        IGameRepository Game { get; }
        void Save();
    }
}
