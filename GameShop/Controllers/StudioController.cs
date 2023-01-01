using GameShop.Models;
using GameShopData.Data;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Controllers
{
    public class StudioController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StudioController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Studio> objfromStudio = _context.Studios;
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
            if(ModelState.IsValid)
            {
                _context.Studios.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //Get Method
        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            var studio = _context.Studios.Find(id);
            if(studio==null)
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
           
            if(ModelState.IsValid)
            {
                _context.Studios.Update(obj);
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

            var studio = _context.Studios.Find(id);
            if(studio == null)
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
            var studio = _context.Studios.Find(id);
            if(studio==null)
            {
                return NotFound();
            }
            _context.Studios.Remove(studio);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
