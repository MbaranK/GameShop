using GameShop.Models;
using GameShopData.Data;
using GameShopDataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudioController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        public StudioController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Studio> objfromStudio = _unitofWork.Studio.GetAll();
            return View(objfromStudio);
        }

        //Get Method
        public IActionResult Add()
        {
            return View();
        }

        //Post Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Studio obj)
        {
            if (ModelState.IsValid)
            {
                _unitofWork.Studio.Add(obj);
                _unitofWork.Save();
                TempData["success"] = "Stüdyo oluşturuldu";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //Get Method
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var studio = _unitofWork.Studio.GetFirstOrDefault(u => u.Id == id);
            if (studio == null)
            {
                return NotFound();
            }
            return View(studio);
        }
        //Post Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Studio obj)
        {

            if (ModelState.IsValid)
            {
                _unitofWork.Studio.update(obj);
                _unitofWork.Save();
                TempData["success"] = "Stüdyo düzenlendi";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //Get Method
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var studio = _unitofWork.Studio.GetFirstOrDefault(u => u.Id == id);
            if (studio == null)
            {
                return NotFound();
            }
            return View(studio);
        }
        //Post Method 
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult DeletePost(int? id)
        {
            var studio = _unitofWork.Studio.GetFirstOrDefault(u => u.Id == id);
            if (studio == null)
            {
                return NotFound();
            }
            _unitofWork.Studio.Remove(studio);
            _unitofWork.Save();
            TempData["success"] = "Stüdyo silindi";
            return RedirectToAction("Index");

        }
    }
}
