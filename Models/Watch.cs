namespace WatchOut.Models
{
    public class Watch
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Size { get; set; }
        public int UserGender { get; set; }
        public string Style { get; set; }
        public int Quantity { get; set; }
        public string PhotoPath { get; set; }

        public Watch()
        {
            
        }
    }
}
