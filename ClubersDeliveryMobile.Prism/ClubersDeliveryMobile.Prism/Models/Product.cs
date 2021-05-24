using Prism.Mvvm;

namespace ClubersDeliveryMobile.Prism.Models
{
    public class Product : BindableBase
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public double Subtotal { get; set; }
    }
}
