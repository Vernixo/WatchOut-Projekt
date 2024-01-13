using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WatchOut.Data;
using WatchOut.Models;

namespace WatchOut.Controllers
{
    public class CartController : Controller
    {
        private readonly WatchOutContext _context;

        public CartController(WatchOutContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }
        
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var watch = _context.Watch.Find(id);
            if (watch != null && quantity > 0)
            {
                var cart = GetCart();
                cart.AddItem(watch, quantity);
                SaveCart(cart);
            }
            return RedirectToAction("Index");
        }
        public IActionResult RemoveFromCart(int watchId)
        {
            var cart = GetCart();
            cart.RemoveItem(watchId);
            SaveCart(cart);
            return RedirectToAction("Index");
        }
        private ShoppingCart GetCart()
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            return cart;
        }

        private void SaveCart(ShoppingCart cart)
        {
            HttpContext.Session.SetObjectAsJson("Cart", cart);
        }
        public IActionResult OrderSuccessful()
        {
            return View("OrderSuccessful");
        }
        [HttpPost]
        //public IActionResult ProcessCheckout(Watch model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var cart = GetCart();
        //        bool isUpdatedSuccessfully = UpdateProductQuantities(cart);

        //        if (isUpdatedSuccessfully)
        //        {
        //            return RedirectToAction("OrderSuccessful");
        //        }
        //        else
        //        {
                    
        //        }
        //    }

        //    return View(model);
        //}
        [HttpPost]
        public IActionResult UpdateProductQuantities(ShoppingCart cart)
        {
            foreach (var item in cart.Items)
            {
                var product = _context.Watch.FirstOrDefault(w => w.Id == item.Watch.Id);
                if (product != null && product.Quantity >= item.Quantity)
                {
                    product.Quantity -= item.Quantity;
                    _context.Update(product);
                }
                else
                {
                    
                }
            }

            _context.SaveChanges();
            return RedirectToAction("OrderSuccessful");
        }


    }

    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }

}
