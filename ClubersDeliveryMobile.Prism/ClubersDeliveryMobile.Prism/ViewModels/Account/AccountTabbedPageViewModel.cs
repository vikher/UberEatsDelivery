using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClubersDeliveryMobile.Prism.ViewModels
{
    public class AccountTabbedPageViewModel : ViewModelBase
    {
        public AccountTabbedPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Mi cuenta";

        }
    }
}
