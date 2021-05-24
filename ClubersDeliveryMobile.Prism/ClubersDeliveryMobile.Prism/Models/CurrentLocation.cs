using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersDeliveryMobile.Prism.Models
{
    public class CurrentLocation
    {
        public int UserId { get; set; }
        public object CurrentLocationLatLng { get; set; }
        public string CurrentLocationAddress { get; set; }
    }
}
