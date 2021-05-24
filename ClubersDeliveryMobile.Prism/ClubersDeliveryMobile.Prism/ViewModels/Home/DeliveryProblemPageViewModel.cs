using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClubersDeliveryMobile.Prism.ViewModels
{
    public class DeliveryProblemPageViewModel : ViewModelBase
    {
        private DelegateCommand _reportProblemCommand;
        private readonly INavigationService _navigationService;

        public DeliveryProblemPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
        }
        public DelegateCommand ReportProblemCommand => _reportProblemCommand ?? (_reportProblemCommand = new DelegateCommand(ReportProblemAsync));

        private async void ReportProblemAsync()
        {
            await _navigationService.NavigateAsync("ReportProblemPage");
        }
    }
}
