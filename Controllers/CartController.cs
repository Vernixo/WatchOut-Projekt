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
            return View(cart); // Przekazuje obiekt typu ShoppingCart do widoku
        }

        [HttpPost]
        [HttpPost]
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var watch = _context.Watch.Find(id); // Upewnij się, że to jest prawidłowa nazwa kolekcji
            if (watch != null && quantity > 0)
            {
                var cart = GetCart();
                cart.AddItem(watch, quantity); // Tutaj przekazujesz 'watch' i 'quantity'
                SaveCart(cart);
            }
            return RedirectToAction("Index");
        }



        private ShoppingCart GetCart()
        {
            // Pobierz koszyk z sesji lub utwórz nowy jeśli nie istnieje
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            return cart;
        }

        private void SaveCart(ShoppingCart cart)
        {
            // Zapisz koszyk do sesji
            HttpContext.Session.SetObjectAsJson("Cart", cart);
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
