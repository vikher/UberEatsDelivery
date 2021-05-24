using ClubersDeliveryMobile.Prism.Models;
using ClubersDeliveryMobile.Prism.Services;
using ClubersDeliveryMobile.Prism.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using Xamarin.Forms;

namespace ClubersDeliveryMobile.Prism.ViewModels
{
    public class PickupNotificationPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private DelegateCommand _continueCommand;
        private CountDown _countDown;

        public PickupNotificationPageViewModel(INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {

            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Timer";
            LoadTimerAsync();
        }

        public CountDown CountDown
        {
            get => _countDown;
            set => SetProperty(ref _countDown, value);
        }

        private string _SecondsElapsed;
        public string SecondsElapsed
        {
            get { return _SecondsElapsed; }
            set { SetProperty(ref _SecondsElapsed, value); }
        }

        public DelegateCommand ContinueCommand => _continueCommand ?? (_continueCommand = new DelegateCommand(ContinueAsync));

        private async void ContinueAsync()
        {
            await _navigationService.NavigateAsync(nameof(PickupDetailsPage));

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            LoadTimerAsync();
        }

        private CountDown GetEvents()
        {
            return new CountDown { Date = new DateTime(DateTime.Now.Ticks + new TimeSpan(0, 0, 0, 5).Ticks) };
        }
        private async void LoadTimerAsync()
        {
            CountDown = GetEvents();
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                if (SecondsElapsed == "00")
                {
                    return false;
                }

                var timespan = CountDown.Date - DateTime.Now;
                CountDown.Timespan = timespan;
                SecondsElapsed = CountDown.Seconds;
                return true;
            });

        }
    }
}
