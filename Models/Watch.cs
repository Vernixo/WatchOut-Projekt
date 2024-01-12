namespace WatchOut.Models
{
    public class Watch
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Size { get; set; }
        public string UserGender { get; set; }
        public string Style { get; set; }
        public int Quantity { get; set; }
        public string PhotoPath { get; set; }

        public Watch()
        {
            
        }
    }
    public class CartItem
    {
        public Watch Watch { get; set; }
        public int Quantity { get; set; }
    }

    public class ShoppingCart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public void AddItem(Watch watch, int quantity)
        {
            // Sprawdź, czy produkt już istnieje w koszyku
            var existingItem = Items.FirstOrDefault(i => i.Watch.Id == watch.Id);
            if (existingItem != null)
            {
                // Jeśli tak, zwiększ ilość
                existingItem.Quantity += quantity;
            }
            else
            {
                // Jeśli nie, dodaj nowy element do koszyka
                Items.Add(new CartItem { Watch = watch, Quantity = quantity });
            }
        }

        // Możesz dodać więcej metod, np. do usunięcia elementu, aktualizacji ilości itd.
    }


}
