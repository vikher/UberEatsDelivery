using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersDeliveryMobile.Prism.Models
{
    public class EarningsInfo
    {
        public DateTime Created { get; set; }
        public string Concept { get; set; }
        public decimal Amount { get; set; }
    }
}
