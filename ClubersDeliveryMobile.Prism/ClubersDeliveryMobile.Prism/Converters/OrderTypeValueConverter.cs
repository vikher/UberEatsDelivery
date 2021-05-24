using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ClubersDeliveryMobile.Prism.Converters
{
    public class OrderTypeValueConverter : IValueConverter
    {
        public string Domicilio = "https://firebasestorage.googleapis.com/v0/b/clubers-278716.appspot.com/o/domicilio.png?alt=media&token=8c960cb2-c17b-46c9-a38c-afab568061f0";
        public string EnSitio = "https://firebasestorage.googleapis.com/v0/b/clubers-278716.appspot.com/o/sitio.png?alt=media&token=c7ab75b0-e56e-4b9c-aee2-632c00f1b996";
        public object Convert(object value, Type targetType, object parameter,
                              CultureInfo culture)
        {
            if (value.ToString() == "Pedido a domicilio")
            {
                return Domicilio;
            }
            if (value.ToString() == "Pedido en sitio")
            {
                return EnSitio;
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
