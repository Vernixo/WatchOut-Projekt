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
            var existingItem = Items.FirstOrDefault(i => i.Watch.Id == watch.Id);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                Items.Add(new CartItem { Watch = watch, Quantity = quantity });
            }
        }
        public void RemoveItem(int watchId)
        {
            var item = Items.FirstOrDefault(i => i.Watch.Id == watchId);
            if (item != null)
            {
                Items.Remove(item);
            }
        }
        public decimal TotalPrice()
        {
            return Items.Sum(item => item.Watch.Price * item.Quantity);
        }
    }


}
