using GameShop.Models;
using GameShopDataAccess.Repository;
using GameShopDataAccess.Repository.IRepository;
using GameShopModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace GameShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitofWork _unitofWork; 

        public HomeController(ILogger<HomeController> logger, IUnitofWork unitofWork)
        {
            _logger = logger;
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Game> gameList = _unitofWork.Game.GetAll(includeProperties: "Category,Studio");
            return View(gameList);
        }

        //Get Method
        public IActionResult Details(int gameId)
        {
            Cart cart = new()
            {
                Count = 1,
                GameId = gameId,
                Game = _unitofWork.Game.GetFirstOrDefault(u => u.Id == gameId, includeProperties: "Category,Studio"),
            };
            return View(cart);
        }

        //Post Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(Cart obj)
        {
            //cart için gameid, applicationuserid ve counta ihtiyacımız var . Gameid ve countu details getden alıyoruz aynı zamanda ara yüzde post methoduna gönderiyoruz. Application User Id yi almak içinse Claims kullanıyorum.

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            obj.ApplicationUserId = claim.Value;

            //Aynı üründen sepete eklediğimiz durumu giriyorum.İlk önce aynı kullanıcı olup olmadığını daha sonra aynı ürün olup olmadığını check ediyorum.
            Cart cartdb = _unitofWork.Cart.GetFirstOrDefault(u => u.ApplicationUserId == claim.Value && u.GameId == obj.GameId);

            if(cartdb == null) // ürün kullanıcı için ilk defa sepete ekleniyor.
            {
                _unitofWork.Cart.Add(obj);
            }
            else
            {
                _unitofWork.Cart.IncreaseCount(cartdb, obj.Count);
            }

            
            _unitofWork.Save();
            TempData[("success")] = "Ürün sepete eklendi";
            return RedirectToAction(nameof(Index));
            //nameof safer way to redirect.
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}