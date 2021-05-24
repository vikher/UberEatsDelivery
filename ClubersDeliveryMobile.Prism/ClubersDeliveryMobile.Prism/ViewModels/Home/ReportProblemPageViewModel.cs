using Prism.Commands;
using Prism.Navigation;

namespace ClubersDeliveryMobile.Prism.ViewModels
{
    public class ReportProblemPageViewModel : ViewModelBase
    {
        private DelegateCommand _goToHomeCommand;
        private readonly INavigationService _navigationService;
        public ReportProblemPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;

        }
        public DelegateCommand GoToHomeCommand => _goToHomeCommand ?? (_goToHomeCommand = new DelegateCommand(GoToHomeAsync));

        private async void GoToHomeAsync()
        {
            await _navigationService.NavigateAsync("/DeliveryMasterDetailPage/NavegationPage/HomePage");
        }
    }
}
