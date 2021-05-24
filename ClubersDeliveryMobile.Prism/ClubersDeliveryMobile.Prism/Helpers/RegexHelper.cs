using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using ClubersDeliveryMobile.Prism.Interfaces;

namespace ClubersDeliveryMobile.Prism.Helpers
{
    public class RegexHelper : IRegexHelper
    {
        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                var mail = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
