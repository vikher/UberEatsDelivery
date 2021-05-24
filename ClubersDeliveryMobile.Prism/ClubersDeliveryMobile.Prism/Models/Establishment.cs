﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersDeliveryMobile.Prism.Models
{
    public class Establishment
    {
        public int EstablishmentId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public LatLng LatLng { get; set; }

    }
}
