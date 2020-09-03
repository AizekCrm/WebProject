using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProject.Interface;
using WebProject.Service.CartServ;

namespace WebProject.Controllers
{
    [Authorize(Roles = "admin, user")]
    public class CartController : Controller
    {
        private readonly IAllMobile AllMobile;
        private readonly CartService CartService;
        private readonly ItemsInCart Products;

        public CartController(
            IAllMobile AllMobile, 
            CartService CartService, 
            ItemsInCart Products)
        {
            this.AllMobile = AllMobile;
            this.CartService = CartService;
            this.Products = Products;
        }

        /// <summary>
        /// Вид Корзины
        /// </summary>
        /// <returns></returns>
        [HttpGet("Cart/Cart")]
        public IActionResult Cart()
        {
            var Items = Products.Products();
            return View(Items);
        }

        /// <summary>
        /// Добавление товара в корзину
        /// </summary>
        [HttpGet("Cart/AddItem")]
        public IActionResult AddItem(int id)
        {
            var SmartPhone = AllMobile.Mobiles.FirstOrDefault(c=>c.PhoneId == id);
            if (Products.Products().Products == null)
            {
                CartService.AddToCart(SmartPhone);
                return RedirectToAction("Cart", "Cart");
            }
            else
            {
                if (Products.Products().Products.Exists(c => c.Exists(c => c.PhoneId == SmartPhone.PhoneId)))
                {
                    CartService.AddToCart(SmartPhone);
                    return RedirectToAction("SmartPhone", "SmartPhone");
                }
                else
                {
                    CartService.AddToCart(SmartPhone);
                    return RedirectToAction("Cart", "Cart");
                }
            }
        }

        /// <summary>
        /// Удаление товара из корзины
        /// </summary>
        [HttpGet("Cart/DeleteItem")]
        public IActionResult DeleteItem(int id)
        {
            var SmartPhone = AllMobile.Mobiles.FirstOrDefault(c => c.PhoneId == id);
            CartService.DeleteToCart(SmartPhone);
            return RedirectToAction("Cart", "Cart");
        }

        /// <summary>
        /// Вернуться к покупкам
        /// </summary>
        /// <returns></returns>
        [HttpGet("Cart/ReturnToBuy")]
        public IActionResult ReturnToBuy()
        {
            return RedirectToAction ("SmartPhone", "SmartPhone");
        }
    }
}
