using ClubersDeliveryMobile.Prism.Services;
using ClubersDeliveryMobile.Prism.Views;
using Prism.Commands;
using Prism.Navigation;

namespace ClubersDeliveryMobile.Prism.ViewModels
{
    public class PaymentConfirmationPageViewModel : ViewModelBase
    {
        
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;

        private DelegateCommand _confirmCommand;
        public PaymentConfirmationPageViewModel(INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _apiService = apiService;
            _navigationService = navigationService;
            Title = "Confirmar pago";
        }
        public DelegateCommand ConfirmCommand => _confirmCommand ?? (_confirmCommand = new DelegateCommand(ConfirmAsync));

        private async void ConfirmAsync()
        {
            await _navigationService.NavigateAsync($"/{nameof(DeliveryMasterDetailPage)}/NavigationPage/{nameof(HomePage)}");

        }
    }
}
