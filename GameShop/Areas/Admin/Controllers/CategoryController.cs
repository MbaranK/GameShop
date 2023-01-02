using GameShop.Models;
using GameShopData.Data;
using GameShopDataAccess.Repository;
using GameShopDataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitofWork _unitofwork;
        public CategoryController(IUnitofWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _unitofwork.Category.GetAll();
            return View(objCategoryList);
        }

        //GET METHOD
        public IActionResult Add()
        {
            return View();
        }

        //POST METHOD

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Category obj)
        {

            if (ModelState.IsValid)
            {
                _unitofwork.Category.Add(obj);
                _unitofwork.Save();
                TempData["success"] = "Tür oluşturuldu";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Tür oluşturulamadı";
            return View(obj);
        }

        //Get Method
        public IActionResult Edit(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _unitofwork.Category.GetFirstOrDefault(u => u.Id == id);
            return View(category);
        }

        //Post Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                _unitofwork.Category.Update(obj);
                _unitofwork.Save();
                TempData["success"] = "Tür düzenlendi";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Tür düzenlenemedi";
            return View(obj);
        }

        //Get Method
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _unitofwork.Category.GetFirstOrDefault(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        //Post Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var category = _unitofwork.Category.GetFirstOrDefault(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _unitofwork.Category.Remove(category);
            _unitofwork.Save();
            TempData["success"] = "Tür Silindi";
            return RedirectToAction("Index");
        }

    }
}
