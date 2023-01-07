using GameShopDataAccess.Repository.IRepository;
using GameShopModels;
using GameShopModels.ViewModel;
using GameShopUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Role.Role_Admin)]
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
            
            return View();
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

        public IActionResult Upsert(GameVM gamevm, IFormFile? file)
        {
            if(ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images/games");
                    var extension = Path.GetExtension(file.FileName);

                    if(gamevm.Game.ImgUrl != null)
                    {
                        var oldImage = Path.Combine(wwwRootPath, gamevm.Game.ImgUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImage))
                        {
                            System.IO.File.Delete(oldImage);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension),FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    gamevm.Game.ImgUrl = @"\images\games\" + fileName + extension;
                }
                if(gamevm.Game.Id == 0)
                {
                    _unitofWork.Game.Add(gamevm.Game);
                }
                else
                {
                    _unitofWork.Game.Update(gamevm.Game);
                }
                _unitofWork.Save();
                TempData["success"] = "Oyun eklendi";
                return RedirectToAction("Index");
            }
            return View(gamevm.Game);
        }

        #region Api Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var gameList = _unitofWork.Game.GetAll(includeProperties: "Category,Studio");

            return Json(new { data = gameList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitofWork.Game.GetFirstOrDefault(u => u.Id == id);
            if(obj == null)
            {
                return Json(new { success = false, message = "Silerken hata oluştu." });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImgUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitofWork.Game.Remove(obj);
            _unitofWork.Save();
            return Json(new { success = true, message = "Silme işlemi gerçekleştirildi." });
            return RedirectToAction("Index");
        }
        #endregion

    }
}
