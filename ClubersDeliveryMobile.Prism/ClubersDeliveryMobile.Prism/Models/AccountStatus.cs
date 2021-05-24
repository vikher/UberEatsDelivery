using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersDeliveryMobile.Prism.Models
{
    public class AccountStatus
    {
        public string Concept { get; set; }
        public double CommissionAmount { get; set; }
        public double ChargeAmount { get; set; }
        public double Amount { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public bool IsAdd { get; set; }

    }
}
