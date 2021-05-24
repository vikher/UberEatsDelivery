using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersDeliveryMobile.Prism.Models
{
    public class CountDown 
    {
        public DateTime Date { get; set; }
        public TimeSpan Timespan { get; set; }
        public string Seconds => Timespan.TotalSeconds.ToString("00");
    }
}
