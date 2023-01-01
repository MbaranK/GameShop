using GameShop.Models;
using GameShopData.Data;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _context.Categories;
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
                       
            if(ModelState.IsValid)
            {
                _context.Categories.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //Get Method
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.Find(id);
            return View(category);
        }

        //Post Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if(ModelState.IsValid)
            {
                _context.Categories.Update(obj);
                _context.SaveChanges();
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
            var category = _context.Categories.Find(id);
            if(category == null)
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
            var category = _context.Categories.Find(id);
            if(category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}
