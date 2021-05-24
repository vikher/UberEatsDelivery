using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersDeliveryMobile.Prism.Models
{
    public abstract class TransactionResult
    {
        public long TotalCount { get; set; }
        public ResultCode ResultCode { get; set; }
        public List<string> ResultMessages { get; set; }
        //public T Result { get; set; }
    }
}
