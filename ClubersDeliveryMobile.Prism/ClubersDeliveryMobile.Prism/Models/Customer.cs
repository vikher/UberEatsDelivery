using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersDeliveryMobile.Prism.Models
{
    public class Customer
    {

        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public LatLng LatLng { get; set; }
    }
}
