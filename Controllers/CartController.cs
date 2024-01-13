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
        
        public IActionResult Checkout()
        {
            return View();
        }

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
        public IActionResult RemoveFromCart(int watchId)
        {
            var cart = GetCart();
            cart.RemoveItem(watchId);
            SaveCart(cart);
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
        [HttpPost]
        public IActionResult ProcessCheckout(Watch model)
        {
            if (ModelState.IsValid)
            {
                var cart = GetCart();
                bool isUpdatedSuccessfully = UpdateProductQuantities(cart);

                if (isUpdatedSuccessfully)
                {
                    // Logika finalizacji zakupu (np. zapisanie zamówienia, wysłanie potwierdzenia itp.)
                    return RedirectToAction("OrderConfirmation");
                }
                else
                {
                    // Obsługa sytuacji, gdy nie udało się zaktualizować ilości (np. produkt niedostępny)
                }
            }

            return View(model);
        }
        [HttpPost]
        public bool UpdateProductQuantities(ShoppingCart cart)
        {
            foreach (var item in cart.Items)
            {
                var product = _context.Watch.FirstOrDefault(w => w.Id == item.Watch.Id);
                if (product != null && product.Quantity >= item.Quantity)
                {
                    product.Quantity -= item.Quantity; // Zmniejszenie ilości
                    _context.Update(product);
                }
                else
                {
                    // Obsługa sytuacji, gdy produkt jest niedostępny lub ilość jest niewystarczająca
                    return false;
                }
            }

            _context.SaveChanges();
            return true;
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
