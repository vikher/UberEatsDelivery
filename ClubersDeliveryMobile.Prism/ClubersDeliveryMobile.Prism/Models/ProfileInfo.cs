using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersDeliveryMobile.Prism.Models
{
    public class ProfileInfo
    {
        public int DeliveryManId { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string VehicleType { get; set; }
        public string TypeOfSuitcase { get; set; }
        public int PercentageOrders { get; set; }
        public int NotifiedOrders { get; set; }
        public int OrdersTaken { get; set; }
        public int RejectedOrders { get; set; }
    }
}
