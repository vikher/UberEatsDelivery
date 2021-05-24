using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubersDeliveryMobile.Prism.Models
{
    public class UserRequest
    {
        [Required]
        public string Document { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        public int UserTypeId { get; set; } // 1: Admin, 2: Provider, 3: DeliveryMan

        public byte[] PictureArray { get; set; }

        public string PasswordConfirm { get; set; }

        //[Required]
        //public string CultureInfo { get; set; }
    }
}
