using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace ClubersDeliveryMobile.Prism.Interfaces
{
	public interface ILocalize
	{
		CultureInfo GetCurrentCultureInfo();

		void SetLocale(CultureInfo ci);
	}
}