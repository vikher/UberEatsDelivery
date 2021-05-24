using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ClubersDeliveryMobile.Prism.Converters
{
    class DateToStringValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                              CultureInfo culture)
        {
            var item = (DateTime)value;
            if (item.DayOfYear == DateTime.Now.DayOfYear)
            {
                return "Hoy";
            }
            if (item.DayOfYear == DateTime.Now.AddDays(-1).DayOfYear)
            {
                return "Ayer";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                                  CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
