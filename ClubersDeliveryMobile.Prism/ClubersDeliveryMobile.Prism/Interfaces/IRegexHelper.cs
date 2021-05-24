using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersDeliveryMobile.Prism.Interfaces
{
    public interface IRegexHelper
    {
        bool IsValidEmail(string emailaddress);
    }
}
