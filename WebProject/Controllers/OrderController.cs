using Microsoft.AspNetCore.Mvc;
using WebProject.Models;
using WebProject.Models.OrderModel;
using WebProject.Service.CartServ;

namespace WebProject.Controllers
{
    public class OrderController : Controller
    {
        private readonly MobileContext db;
        private readonly ItemsInCart Items;

        public OrderController (
            MobileContext db, 
            ItemsInCart Items)
        {
            this.Items = Items; 
            this.db = db;
        }

        [HttpGet("Order/Order")]
        public IActionResult Order()
        {
            return View();
        }

        [HttpPost("Order/OrderConfirm")]
        public IActionResult OrderConfirm(Order Order)
        {
            if ((Order.CustomerName == Order.CustomerAddress) && (!string.IsNullOrEmpty(Order.CustomerName) && !string.IsNullOrEmpty(Order.CustomerAddress)))
            {
                ModelState.AddModelError("", "Имя и адресс не должны совпадать");
            }
            if (ModelState.IsValid)
            {
                Items.GetItemForOrder(Order);
                db.Cart.Add(Order.Cart);
                db.Orders.Add(Order);
                db.SaveChanges();
                HttpContext.Session.Remove("Item");
                return RedirectToAction("ThankForBuy");
            }
            else
            {
                return View(Order);
            }
        }

        [HttpGet("Order/ThankForBuy")]
        public IActionResult ThankForBuy()
        {
            return View();
        }
    }
}
