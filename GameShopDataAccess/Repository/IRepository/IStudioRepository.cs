﻿using GameShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopDataAccess.Repository.IRepository
{
    public interface IStudioRepository : IRepository<Studio>
    {
        void update(Studio studio);
    }
}
