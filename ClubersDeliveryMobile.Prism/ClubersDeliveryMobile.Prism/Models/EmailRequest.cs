using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubersDeliveryMobile.Prism.Models
{
    public class EmailRequest
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }

        //[Required]
        //public string CultureInfo { get; set; }
    }
}
