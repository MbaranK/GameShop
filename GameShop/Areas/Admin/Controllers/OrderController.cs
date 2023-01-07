using GameShopDataAccess.Repository.IRepository;
using GameShopModels;
using GameShopModels.ViewModel;
using GameShopUtility;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        [BindProperty]
        public OrderVM orderVM { get; set; }
        public OrderController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public IActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<OrderHeader> orderHeaders;
            if(User.IsInRole(Role.Role_Admin))
            {
                orderHeaders = _unitofWork.OrderHeader.GetAll(includeProperties: "ApplicationUser");
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                orderHeaders = _unitofWork.OrderHeader.GetAll(u => u.ApplicationUserId == claim.Value, includeProperties: "ApplicationUser");
            }

            return Json(new { data = orderHeaders});
        }
    }

    
}
