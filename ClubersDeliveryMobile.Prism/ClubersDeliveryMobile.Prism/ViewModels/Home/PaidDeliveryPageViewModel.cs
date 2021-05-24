using ClubersDeliveryMobile.Prism.Helpers;
using ClubersDeliveryMobile.Prism.Models;
using ClubersDeliveryMobile.Prism.Services;
using ClubersDeliveryMobile.Prism.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;

namespace ClubersDeliveryMobile.Prism.ViewModels
{
    public class PaidDeliveryPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;

        private DelegateCommand _navigateToEstablishmentCommand;
        private List<Delivery> _deliveries;
        private List<Product> _products;
        private Order _order;
        private bool _isEndDeliveryVisible = false;
        private DelegateCommand _arrivedCommand;
        private bool _isDeliveryStarted;

        private DelegateCommand _deliveryProblemCommand;
        private DelegateCommand _finishDeliveryCommand;
        private DelegateCommand _backgroundClickedCommand;
        private DelegateCommand _chatCommand;
        private string _chatIcon = IconFont.Message;

        private readonly IApiService _apiService;
        private double _OverlayOpacity;
        private bool _IsExpanded = false;
        private bool _IsVisible = true;

        private const double MaxOpacity = 1;
        private double _ExpandedPercentage;
        public PaidDeliveryPageViewModel(INavigationService navigationService,
            IApiService apiService, IPageDialogService dialogService) : base(navigationService)
        {
            _apiService = apiService;
            _dialogService = dialogService;
            _navigationService = navigationService;
            Title = "Comenzando";
            IsDeliveryStarted = false;

        }
        public DelegateCommand NavigateToEstablishmentCommand => _navigateToEstablishmentCommand ?? (_navigateToEstablishmentCommand = new DelegateCommand(NavigateToEstablishment));
        public DelegateCommand BackgroundClickedCommand => _backgroundClickedCommand ?? (_backgroundClickedCommand = new DelegateCommand(BackgroundClicked));
        public DelegateCommand FinishDeliveryCommand => _finishDeliveryCommand ?? (_finishDeliveryCommand = new DelegateCommand(FinishDeliveryAsync));
        public DelegateCommand ArrivedCommand => _arrivedCommand ?? (_arrivedCommand = new DelegateCommand(ArrivedAsync));
        public DelegateCommand ChatCommand => _chatCommand ?? (_chatCommand = new DelegateCommand(ChatAsync));
        public DelegateCommand DeliveryProblemCommand => _deliveryProblemCommand ?? (_deliveryProblemCommand = new DelegateCommand(DeliveryProblemAsync));
        public bool IsDeliveryStarted
        {
            get => _isDeliveryStarted;
            set => SetProperty(ref _isDeliveryStarted, value);
        }
        private async void ChatAsync()
        {
            await _navigationService.NavigateAsync(nameof(MessagesPage));
        }
        public string ChatIcon
        {
            get => _chatIcon;
            set => SetProperty(ref _chatIcon, value);
        }

        public double OverlayOpacity
        {
            get => _OverlayOpacity;
            set => SetProperty(ref _OverlayOpacity, value);
        }
        public double ExpandedPercentage
        {
            get => _ExpandedPercentage;
            set
            {
                SetProperty(ref _ExpandedPercentage, value);
                OverlayOpacity = MaxOpacity < value ? MaxOpacity : value;
            }
        }
        public bool IsExpanded
        {
            get => _IsExpanded;
            set => SetProperty(ref _IsExpanded, value);
        }

        public bool IsVisible
        {
            get => _IsVisible;
            set => SetProperty(ref _IsVisible, value);
        }
        public bool IsEndDeliveryVisible
        {
            get => _isEndDeliveryVisible;
            set => SetProperty(ref _isEndDeliveryVisible, value);
        }
        private void BackgroundClicked()
        {
            IsExpanded = false;
        }
        private async void NavigateToEstablishment()
        {

            var location = new Location(Order.Customer.LatLng.lat, Order.Customer.LatLng.lng);
            var options = new MapLaunchOptions { Name = Order.Customer.Name, NavigationMode = Xamarin.Essentials.NavigationMode.Driving };
            await Map.OpenAsync(location, options);
        }

        public List<Delivery> Deliveries
        {
            get => _deliveries;
            set => SetProperty(ref _deliveries, value);
        }
        public Order Order
        {
            get => _order;
            set => SetProperty(ref _order, value);
        }
        public List<Product> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }
        private async void FinishDeliveryAsync()
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

            Response1<bool> response = await _apiService.FinalizePaidOrder(Constants.urlBase, Constants.servicePrefix, Constants.FinalizePaidOrderAppSRAsyncController, Constants.tokenType, token.Result.token, Order.OrderId);

            if (response.ResultCode != ResultCode.Success)
            {
                IsRunning = false;
                IsEnabled = true;
                await _dialogService.DisplayAlertAsync(Constants.ErrorMessage, response.ResultMessages.FirstOrDefault(), Constants.AcceptMessage);
                return;
            }

            IsRunning = false;
            IsEnabled = true;
            Settings.IsDeliveryStarted = IsDeliveryStarted;

            await _navigationService.NavigateAsync($"/{nameof(DeliveryMasterDetailPage)}/NavigationPage/{nameof(HomePage)}");

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("order"))
            {
                Order = parameters.GetValue<Order>("order");
                Products = Order.Products;
            }
        }

        private void ArrivedAsync()
        {
            IsEndDeliveryVisible = true;
        }
        private async void DeliveryProblemAsync()
        {
            await _navigationService.NavigateAsync(nameof(SupportPage));
        }
    }
}
