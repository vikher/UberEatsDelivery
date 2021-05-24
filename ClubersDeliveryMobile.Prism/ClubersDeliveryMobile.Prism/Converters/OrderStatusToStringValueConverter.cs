using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ClubersDeliveryMobile.Prism.Converters
{
    public class OrderStatusToStringValueConverter : IValueConverter
    {
        public string Domicilio = "Pedido a domicilio";
        public string EnSitio = "En sitio";
        public string Cancelado = "Cancelado";
        public object Convert(object value, Type targetType, object parameter,
                              CultureInfo culture)
        {
            if (value.ToString() == "Pedido a domicilio")
            {
                return Domicilio;
            }
            if (value.ToString() == "onsite")
            {
                return EnSitio;
            }
            if (value.ToString() == "cancelled")
            {
                return Cancelado;
            }
            return String.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                                  CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
