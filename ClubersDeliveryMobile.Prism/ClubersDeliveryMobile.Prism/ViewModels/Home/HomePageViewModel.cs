using ClubersDeliveryMobile.Prism.Helpers;
using ClubersDeliveryMobile.Prism.Models;
using ClubersDeliveryMobile.Prism.Services;
using ClubersDeliveryMobile.Prism.Views;
using ClubersDeliveryMobile.Prism.Views.Home;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Linq;
using Xamarin.Forms;

namespace ClubersDeliveryMobile.Prism.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;
        private string _menuIcon = IconFont.Menu;
        private string _SecondsElapsed;
        private bool _isVisible;
        private bool _isAvailable;
        private string _orderId;
        private CountDown _countDown;
        private DeliveryMenStatus _deliveryMenStatus;
        private DelegateCommand _continueCommand;
        private DelegateCommand _rejectCommand;
        private DelegateCommand _toggledCommand;
        private DelegateCommand _acceptDeliveryCommand;

        public HomePageViewModel(INavigationService navigationService, IApiService apiService, IPageDialogService dialogService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _dialogService = dialogService;
            Title = "Map";
            IsVisible = false;
            LoadStatusDeliveryMenAsync();
        }

        public DelegateCommand ToggledCommand => _toggledCommand ?? (_toggledCommand = new DelegateCommand(ModifyIsAvailableAsync));
        public DelegateCommand RejectCommand => _rejectCommand ?? (_rejectCommand = new DelegateCommand(RejectAsync));
        public DelegateCommand ContinueCommand => _continueCommand ?? (_continueCommand = new DelegateCommand(ContinueAsync));
        public DelegateCommand AcceptDeliveryCommand => _acceptDeliveryCommand ?? (_acceptDeliveryCommand = new DelegateCommand(AcceptDeliveryAsync));

        public bool IsVisible
        {
            get { return _isVisible; }
            set { SetProperty(ref _isVisible, value); }
        }
        public DeliveryMenStatus DeliveryMenStatus
        {
            get { return _deliveryMenStatus; }
            set { SetProperty(ref _deliveryMenStatus, value); }
        }
        public bool IsAvailable
        {
            get => _isAvailable;
            set
            {
                SetProperty(ref _isAvailable, value);
            }
        }
        public CountDown CountDown
        {
            get => _countDown;
            set => SetProperty(ref _countDown, value);
        }

        public string SecondsElapsed
        {
            get { return _SecondsElapsed; }
            set { SetProperty(ref _SecondsElapsed, value); }
        }
        public string MenuIcon
        {
            get => _menuIcon;
            set => SetProperty(ref _menuIcon, value);
        }
        public string OrderId
        {
            get { return _orderId; }
            set { SetProperty(ref _orderId, value); }
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            
            MessagingCenter.Subscribe<object, string>(this, "RecibiendoNuevoPedido", (sender, msg) =>
            {
                OrderId = msg;
                IsVisible = true;
                LoadTimerAsync();
            });
        }
        private async void AcceptDeliveryAsync()
        {
            await _navigationService.NavigateAsync($"/{nameof(DeliveryMasterDetailPage)}/NavigationPage/{nameof(PickupNotificationPage)}");
        }
        private async void ContinueAsync()
        {
            AcceptOrderAsync();
            IsVisible = false;
            SecondsElapsed = "60";
            await _navigationService.NavigateAsync(nameof(PickupDetailsPage));
        }

        private CountDown GetEvents()
        {
            return new CountDown { Date = new DateTime(DateTime.Now.Ticks + new TimeSpan(0, 0, 1, 1).Ticks) };
        }
        private async void LoadTimerAsync()
        {
            CountDown = GetEvents();
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                if (SecondsElapsed == "00")
                {
                    IsVisible = false;
                    RejectOrderAsync();
                    SecondsElapsed = "60";
                    return false;
                }

                var timespan = CountDown.Date - DateTime.Now;
                CountDown.Timespan = timespan;
                SecondsElapsed = CountDown.Seconds;
                return true;
            });

        }
        private void RejectAsync()
        {
            IsVisible = false;
            RejectOrderAsync();
            SecondsElapsed = "60";
        }

        private async void LoadStatusDeliveryMenAsync()
        {
            IsRunning = true;
            if (!_apiService.CheckConnection())
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert(Constants.ErrorMessage, Constants.ConnectionError, Constants.AcceptMessage);
                return;
            }

            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            UserResponse User = JsonConvert.DeserializeObject<UserResponse>(Settings.User);

            Response1<DeliveryMenStatus> response = await _apiService.GetStatusDeliveryMen(Constants.urlBase, Constants.servicePrefix, Constants.GetStatusDeliveryMenAppSRAsyncController, Constants.tokenType, token.Result.token, User.Id);

            if (response.ResultCode != ResultCode.Success)
            {
                await App.Current.MainPage.DisplayAlert(Constants.ErrorMessage, response.ResultMessages.FirstOrDefault(), Constants.AcceptMessage);
                return;
            }

            DeliveryMenStatus = response.Result;
            IsAvailable = DeliveryMenStatus.Status;

            IsRunning = false;

        }

        private async void ModifyIsAvailableAsync()
        {
                IsRunning = true;
                IsEnabled = false;

                if (!_apiService.CheckConnection())
                {
                    IsRunning = false;
                    IsEnabled = true;
                    await _dialogService.DisplayAlertAsync(Constants.ErrorMessage, Constants.ConnectionError, Constants.AcceptMessage);
                    return;
                }

                TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
                UserResponse User = JsonConvert.DeserializeObject<UserResponse>(Settings.User);

                Response1<bool> response = await _apiService.UpdateIsAvailable(Constants.urlBase, Constants.servicePrefix, Constants.UpdateStatusDeliveryManAsyncController, IsAvailable, Constants.tokenType, token.Result.token, User.Id);

                if (response.ResultCode != ResultCode.Success)
                {
                    IsRunning = false;
                    IsEnabled = true;
                    await _dialogService.DisplayAlertAsync(Constants.ErrorMessage, response.ResultMessages.FirstOrDefault(), Constants.AcceptMessage);
                    return;
                }

                IsRunning = false;
                IsEnabled = true;
        }
        private async void AcceptOrderAsync()
        {
            IsRunning = true;
            IsEnabled = false;

            if (!_apiService.CheckConnection())
            {
                IsRunning = false;
                IsEnabled = true;
                await _dialogService.DisplayAlertAsync(Constants.ErrorMessage, Constants.ConnectionError, Constants.AcceptMessage);
                return;
            }

            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            UserResponse User = JsonConvert.DeserializeObject<UserResponse>(Settings.User);

            Response1<bool> response = await _apiService.AcceptOrder(Constants.urlBase, Constants.servicePrefix, Constants.AcceptOrderAppSRAsyncController, OrderId, Constants.tokenType, token.Result.token, DeliveryMenStatus.DeliveryManId);

            if (response.ResultCode != ResultCode.Success)
            {
                IsRunning = false;
                IsEnabled = true;
                await _dialogService.DisplayAlertAsync(Constants.ErrorMessage, response.ResultMessages.FirstOrDefault(), Constants.AcceptMessage);
                return;
            }

            IsRunning = false;
            IsEnabled = true;
            IsVisible = false;

        }
        private async void RejectOrderAsync()
        {
            IsRunning = true;
            IsEnabled = false;

            if (!_apiService.CheckConnection())
            {
                IsRunning = false;
                IsEnabled = true;
                await _dialogService.DisplayAlertAsync(Constants.ErrorMessage, Constants.ConnectionError, Constants.AcceptMessage);
                return;
            }

            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            UserResponse User = JsonConvert.DeserializeObject<UserResponse>(Settings.User);

            Response1<bool> response = await _apiService.RejectOrder(Constants.urlBase, Constants.servicePrefix, Constants.RejectOrderAppSRAsyncController, OrderId, Constants.tokenType, token.Result.token, DeliveryMenStatus.DeliveryManId);

            if (response.ResultCode != ResultCode.Success)
            {
                IsRunning = false;
                IsEnabled = true;
                await _dialogService.DisplayAlertAsync(Constants.ErrorMessage, response.ResultMessages.FirstOrDefault(), Constants.AcceptMessage);
                return;
            }

            IsRunning = false;
            IsEnabled = true;
        }
    }
}
