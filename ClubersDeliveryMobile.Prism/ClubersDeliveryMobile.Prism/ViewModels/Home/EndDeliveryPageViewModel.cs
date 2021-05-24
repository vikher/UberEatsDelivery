using ClubersDeliveryMobile.Prism.Models;
using ClubersDeliveryMobile.Prism.Services;
using ClubersDeliveryMobile.Prism.Views;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;

namespace ClubersDeliveryMobile.Prism.ViewModels
{
    public class EndDeliveryPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private List<Delivery> _deliveries;
        private DelegateCommand _deliveryProblemCommand;
        private DelegateCommand _endDeliveryCommand;
        private readonly IApiService _apiService;
        public EndDeliveryPageViewModel(INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _apiService = apiService;
            _navigationService = navigationService;
            Title = "Llegando";
        }

        public DelegateCommand EndDeliveryCommand => _endDeliveryCommand ?? (_endDeliveryCommand = new DelegateCommand(EndDeliveryAsync));
        public DelegateCommand DeliveryProblemCommand => _deliveryProblemCommand ?? (_deliveryProblemCommand = new DelegateCommand(DeliveryProblemAsync));
        private async void DeliveryProblemAsync()
        {
            await _navigationService.NavigateAsync("DeliveryProblemPage");
        }
        public List<Delivery> Deliveries
        {
            get => _deliveries;
            set => SetProperty(ref _deliveries, value);
        }

        private async void EndDeliveryAsync()
        {
            await _navigationService.NavigateAsync($"/{nameof(DeliveryMasterDetailPage)}/NavigationPage/{nameof(HomePage)}");
        }
    }
}
