namespace CarShop.Models.CarAttributes
{
    public class Engine : Base.CarAttributes
    {
        public float Volume { get; set; }
        public int Power { get; set; }
        public string Fuel {get; set; }
    }
}