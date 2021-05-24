using ClubersDeliveryMobile.Prism.Models;
using ClubersDeliveryMobile.Prism.Services;
using ClubersDeliveryMobile.Prism.Views;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;

namespace ClubersDeliveryMobile.Prism.ViewModels
{
    public class StartDeliveryPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private List<Delivery> _deliveries;
        private DelegateCommand _deliveryProblemCommand;
        private DelegateCommand _startDeliveryCommand;
        private readonly IApiService _apiService;
        public StartDeliveryPageViewModel(INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _apiService = apiService;
            _navigationService = navigationService;
            Title = "Comenzando";
        }

        public DelegateCommand StartDeliveryCommand => _startDeliveryCommand ?? (_startDeliveryCommand = new DelegateCommand(StartDeliveryAsync));
        public DelegateCommand DeliveryProblemCommand => _deliveryProblemCommand ?? (_deliveryProblemCommand = new DelegateCommand(DeliveryProblemAsync));

        private async void DeliveryProblemAsync()
        {
            await _navigationService.NavigateAsync("DeliveryProblemPage");
        }

        private async void StartDeliveryAsync()
        {
            await _navigationService.NavigateAsync($"/{nameof(DeliveryMasterDetailPage)}/NavigationPage/{nameof(EndDeliveryPage)}");

        }

        public List<Delivery> Deliveries
        {
            get => _deliveries;
            set => SetProperty(ref _deliveries, value);
        }

    }
}
