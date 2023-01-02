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
    public class GameRepository : Repository<Game>, IGameRepository
    {
        private ApplicationDbContext _context;
        public GameRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Game game)
        {
            //Diğer şekilde yapmaktansa bu şekilde yapmak daha mantıklı çünkü bir çok property var birini güncellediğimizde hepsi güncellenecekti. Bunun önüne geçmiş olduk.
            var objFromGame = _context.Games.FirstOrDefault(u => u.Id == game.Id);
            if(objFromGame != null)
            {
                objFromGame.Name = game.Name;
                objFromGame.Description = game.Description;
                objFromGame.Description = game.Description;
                objFromGame.Price = game.Price;
                objFromGame.CategoryId = game.CategoryId;
                objFromGame.StudioId = game.StudioId;
                objFromGame.Stock = game.Stock;

                if(objFromGame.ImgUrl != null)
                {
                    objFromGame.ImgUrl = game.ImgUrl;
                }
            }
        }
    }
}
