using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClubersDeliveryMobile.Prism.Models
{
    public class Order : BindableBase
    {
        public string PaymentMethod { get; set; }
        public int Quantity { get; set; }
        public StatusName StatusName { get; set; }
        public int OrderId { get; set; }
        public string ConsumptionTypeName { get; set; }
        public object NumberOfPackagesDelivered { get; set; }
        public double TipAmount { get; set; }
        public double DeliveryFee { get; set; }
        public double Total { get; set; }
        public Customer Customer { get; set; }
        public List<Product> Products { get; set; }
        public Establishment Establishment { get; set; }

    }
}
