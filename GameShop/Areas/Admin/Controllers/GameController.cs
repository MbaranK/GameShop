using GameShopDataAccess.Repository.IRepository;
using GameShopModels;
using GameShopModels.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GameController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public GameController(IUnitofWork unitofWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitofWork = unitofWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Game> games = _unitofWork.Game.GetAll();
            return View(games);
        }

        //Get Method Update ve Insert methodu birlikte kullanım.
        public IActionResult Upsert(int? id)
        {
            GameVM gameVM = new()
            {
                Game = new(),
                CategoryList = _unitofWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                StudioList = _unitofWork.Studio.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };

            if(id == null || id == 0)
            {
                return View(gameVM);
            }
            else
            {
                gameVM.Game = _unitofWork.Game.GetFirstOrDefault(u => u.Id == id);
                return View(gameVM);
            }
        }

        //Post Method
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(GameVM gamevm, IFormFile file)
        {
            if(ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images/games");
                    var extension = Path.GetExtension(file.FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension),FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    gamevm.Game.ImgUrl = @"\images\games\" + fileName + extension;
                }
                _unitofWork.Game.Add(gamevm.Game);
                _unitofWork.Save();
                TempData["success"] = "Oyun eklendi";
                return RedirectToAction("Index");
            }
            return View(gamevm.Game);
        }

    }
}
