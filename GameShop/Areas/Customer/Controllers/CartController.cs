using GameShopDataAccess.Repository.IRepository;
using GameShopModels;
using GameShopModels.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        [BindProperty]
        public CartVM CartVM { get; set; }
        public CartController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            //application user idyi getirdim.
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            CartVM = new CartVM()
            {
                CartList = _unitofWork.Cart.GetAll(u => u.ApplicationUserId == claim.Value, includeProperties: "Game"),
                OrderHeader = new()
            };

            foreach(var item in CartVM.CartList)
            {
                item.Price = item.Count * item.Game.Price;
                CartVM.OrderHeader.OrderTotal += item.Price;
            }

            return View(CartVM);
        }

        public IActionResult Plus(int cartid)
        {
            var cart = _unitofWork.Cart.GetFirstOrDefault(u => u.Id == cartid);
            _unitofWork.Cart.IncreaseCount(cart, 1);
            _unitofWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartid)
        {
            var cart = _unitofWork.Cart.GetFirstOrDefault(u => u.Id == cartid);
            _unitofWork.Cart.DecreaseCount(cart, 1);
            if(cart.Count < 1)
            {
                _unitofWork.Cart.Remove(cart);
            }
            _unitofWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int cartid)
        {
            var cart = _unitofWork.Cart.GetFirstOrDefault(u => u.Id == cartid);
            _unitofWork.Cart.Remove(cart);
            _unitofWork.Save();
            return RedirectToAction(nameof(Index));
        }
        //Get METHOD
        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            CartVM = new CartVM()
            {
                CartList = _unitofWork.Cart.GetAll(u => u.ApplicationUserId == claim.Value, includeProperties: "Game"),
                OrderHeader = new()
            };

            CartVM.OrderHeader.ApplicationUser = _unitofWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);
            CartVM.OrderHeader.Name = CartVM.OrderHeader.ApplicationUser.Name;
            CartVM.OrderHeader.PhoneNumber= CartVM.OrderHeader.ApplicationUser.PhoneNumber;
            CartVM.OrderHeader.Address = CartVM.OrderHeader.ApplicationUser.Address;
            CartVM.OrderHeader.City = CartVM.OrderHeader.ApplicationUser.City;
            CartVM.OrderHeader.State = CartVM.OrderHeader.ApplicationUser.State;

            foreach (var item in CartVM.CartList)
            {
                item.Price = item.Count * item.Game.Price;
                CartVM.OrderHeader.OrderTotal += item.Price;
            }

            return View(CartVM);
        }

        //POST METHOD
        [HttpPost]
        [ActionName("Summary")]
        [ValidateAntiForgeryToken]
        public IActionResult SummaryPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            CartVM.CartList = _unitofWork.Cart.GetAll(u => u.ApplicationUserId == claim.Value, includeProperties: "Game");

            CartVM.OrderHeader.OrderDate = System.DateTime.Now;
            CartVM.OrderHeader.ApplicationUserId = claim.Value;

            foreach (var item in CartVM.CartList)
            {
                item.Price = item.Count * item.Game.Price;
                CartVM.OrderHeader.OrderTotal += item.Price;
            }

            _unitofWork.OrderHeader.Add(CartVM.OrderHeader);
            _unitofWork.Save();

            foreach (var item in CartVM.CartList)
            {
                OrderDetails orderDetails = new()
                {
                    GameId = item.GameId,
                    OrderId = CartVM.OrderHeader.Id,
                    Price = item.Price,
                    Count = item.Count
                };
                _unitofWork.OrderDetail.Add(orderDetails);
                _unitofWork.Save();
            }
            // Listeyi boşaltma işlemi
            _unitofWork.Cart.RemoveRange(CartVM.CartList);
            _unitofWork.Save();
            TempData["success"] = "Sipariş oluşturuldu.";
            return RedirectToAction("Index","Home");
        }


    }
}
