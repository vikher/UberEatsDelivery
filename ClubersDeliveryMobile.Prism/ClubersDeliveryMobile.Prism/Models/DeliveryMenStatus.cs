using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersDeliveryMobile.Prism.Models
{
    public class DeliveryMenStatus
    {
        public int DeliveryManId { get; set; }
        public int UserId { get; set; }
        public bool Status { get; set; }
    }
}
