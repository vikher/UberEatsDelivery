using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersDeliveryMobile.Prism.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string TransactionType { get; set; }

        public int Charge { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int CommissionAmount { get; set; } // TODO: Move to order
        public int TipAmount { get; set; } // TODO: Move to order
        public Order Order { get; set; }
        public bool Status { get; set; }
        public int EarningsTotal => TipAmount + CommissionAmount;

    }
}
