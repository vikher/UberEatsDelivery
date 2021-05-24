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
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace ClubersDeliveryMobile.Prism.ViewModels
{
    public class PickupDetailsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IGeolocatorService _geolocatorService;
        private readonly IApiService _apiService;
        private readonly IPageDialogService _dialogService;
        private DelegateCommand _navigateToEstablishmentCommand;
        private DelegateCommand _getAddressCommand;
        private List<Delivery> _deliveries;
        private List<Product> _products;
        private Order _order;
        private bool _isSecondButtonVisible;
        private DelegateCommand _deliveryProblemCommand;
        private DelegateCommand _startDeliveryCommand;
        private DelegateCommand _backgroundClickedCommand;
        private Position _position;
        private TripResponse _tripResponse;
        private Timer _timer;
        private Geocoder _geoCoder;
        private string _chatIcon = IconFont.Message;
        private string _source;
        private string _buttonLabel;
        private const double MaxOpacity = 1;
        private double _ExpandedPercentage;
        private TripDetailsRequest _tripDetailsRequest;
        private DelegateCommand _chatCommand;
        private bool _IsExpanded = false;
        private double _OverlayOpacity;
        private bool _IsVisible = true;
        private bool _isDeliveryStarted;

        public PickupDetailsPageViewModel(INavigationService navigationService, IGeolocatorService geolocatorService,
            IApiService apiService, IPageDialogService dialogService) : base(navigationService)
        {
            _apiService = apiService;
            _dialogService = dialogService;
            _navigationService = navigationService;
            _geolocatorService = geolocatorService;
            _tripDetailsRequest = new TripDetailsRequest { TripDetails = new List<TripDetailRequest>() };
            Title = "Comenzando";
            LoadOrderDetails();
            LoadSourceAsync();
            IsDeliveryStarted = true;

        }
        public DelegateCommand NavigateToEstablishmentCommand => _navigateToEstablishmentCommand ?? (_navigateToEstablishmentCommand = new DelegateCommand(NavigateToEstablishment));
        public DelegateCommand BackgroundClickedCommand => _backgroundClickedCommand ?? (_backgroundClickedCommand = new DelegateCommand(BackgroundClicked));
        public DelegateCommand StartDeliveryCommand => _startDeliveryCommand ?? (_startDeliveryCommand = new DelegateCommand(StartDeliveryAsync));
        public DelegateCommand DeliveryProblemCommand => _deliveryProblemCommand ?? (_deliveryProblemCommand = new DelegateCommand(DeliveryProblemAsync));
        public DelegateCommand ChatCommand => _chatCommand ?? (_chatCommand = new DelegateCommand(ChatAsync));

        public bool IsDeliveryStarted
        {
            get => _isDeliveryStarted;
            set => SetProperty(ref _isDeliveryStarted, value);
        }
        public List<Product> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
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
        public bool IsSecondButtonVisible
        {
            get => _isSecondButtonVisible;
            set => SetProperty(ref _isSecondButtonVisible, value);
        }
        public string Source
        {
            get => _buttonLabel;
            set => SetProperty(ref _buttonLabel, value);
        }

        public string ButtonLabel
        {
            get => _source;
            set => SetProperty(ref _source, value);
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
        private void BackgroundClicked()
        {
            IsExpanded = false;
        }
        private async void NavigateToEstablishment()
        {
            var location = new Location(Order.Establishment.LatLng.lat, Order.Establishment.LatLng.lng);
            var options = new MapLaunchOptions { Name = Order.Establishment.Name, NavigationMode = Xamarin.Essentials.NavigationMode.Driving };

            await Xamarin.Essentials.Map.OpenAsync(location, options);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (_timer != null)
            {
                _timer.Start();
            }
            StartTripAsync();
        }
        private async void DeliveryProblemAsync()
        {
            await _navigationService.NavigateAsync(nameof(SupportPage));
        }
        private async void ChatAsync()
        {
            await _navigationService.NavigateAsync(nameof(MessagesPage));
        }
        private async void StartDeliveryAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "order", Order }
            };

            if (Order.PaymentMethod == "Venta Efectivo")
            {
                await _navigationService.NavigateAsync(nameof(NonPaidDeliveryPage), parameters);
            }
            else
            {
                await _navigationService.NavigateAsync(nameof(PaidDeliveryPage), parameters);
            }
        }

        private async void StartTripAsync()
        {
            await BeginTripAsync();
        }

        private async void LoadSourceAsync()
        {
            IsEnabled = false;
            await _geolocatorService.GetLocationAsync();

            if (_geolocatorService.Latitude == 0 && _geolocatorService.Longitude == 0)
            {
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Constants.ErrorMessage, "No es posible obtener la direccion actual, por favor intente más tarde", Constants.AcceptMessage);
                await _navigationService.GoBackAsync();
                return;
            }

            _position = new Position(_geolocatorService.Latitude, _geolocatorService.Longitude);
            Geocoder geoCoder = new Geocoder();
            IEnumerable<string> sources = await geoCoder.GetAddressesForPositionAsync(_position);
            List<string> addresses = new List<string>(sources);

            if (addresses.Count > 1)
            {
                Source = addresses[0];
            }

            IsEnabled = true;
        }

        private async void LoadOrderDetails()
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

            Response1<Order> response = await _apiService.GetOrderDetails(Constants.urlBase, Constants.servicePrefix, Constants.GetOrderDetailAppSRAsyncController, Constants.tokenType, token.Result.token, User.Id);

            if (response.ResultCode != ResultCode.Success)
            {
                await App.Current.MainPage.DisplayAlert(Constants.ErrorMessage, response.ResultMessages.FirstOrDefault(), Constants.AcceptMessage);
                return;
            }

            Order = response.Result;
            Products = Order.Products;

            IsRunning = false;
            Settings.IsDeliveryStarted = IsDeliveryStarted;
        }

        private async Task BeginTripAsync()
        {
            IsRunning = true;
            IsEnabled = false;

            if (!_apiService.CheckConnection())
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert(Constants.ErrorMessage, Constants.ConnectionError, Constants.AcceptMessage);
                return;
            }

            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            UserResponse User = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            ButtonLabel = "End trip";
            PickupDetailsPage.GetInstance().AddPin(_position, Source, "Star trip", PinType.Place);
            IsRunning = false;
            IsEnabled = true;

            _timer = new Timer
            {
                Interval = 1000*60
            };

            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
        }

        private async Task EndTripAsync()
        {
            _timer.Stop();

            if (_tripDetailsRequest.TripDetails.Count > 0)
            {
                await SendTripDetailsAsync();
            }

            NavigationParameters parameters = new NavigationParameters
            {
                { "tripId", _tripResponse.Id },
            };
        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            await _geolocatorService.GetLocationAsync();
            if (_geolocatorService.Latitude == 0 && _geolocatorService.Longitude == 0)
            {
                return;
            }

            Position previousPosition = new Position(_position.Latitude, _position.Longitude);
            _position = new Position(_geolocatorService.Latitude, _geolocatorService.Longitude);
            double distance = GeoHelper.GetDistance(previousPosition, _position, UnitOfLength.Kilometers);

            if (distance < 0.003 || double.IsNaN(distance))
            {
                return;
            }

            MainThread.BeginInvokeOnMainThread(() =>
            {
                PickupDetailsPage.GetInstance().DrawLine(previousPosition, _position);
            });

            SetCurrentLocationAsync();
        }

        private async Task SendTripDetailsAsync()
        {
            TripDetailsRequest tripDetailsRequestCloned = CloneTripDetailsRequest(_tripDetailsRequest);
            _tripDetailsRequest.TripDetails.Clear();
            await _apiService.AddTripDetailsAsync(Constants.urlBase, "/api", "/Trips/AddTripDetails", tripDetailsRequestCloned, "bearer", "dfghjk");
        }

        private TripDetailsRequest CloneTripDetailsRequest(TripDetailsRequest tripDetailsRequest)
        {
            TripDetailsRequest tripDetailsRequestCloned = new TripDetailsRequest
            {
                TripDetails = tripDetailsRequest.TripDetails.Select(d => new TripDetailRequest
                {
                    Address = d.Address,
                    Latitude = d.Latitude,
                    Longitude = d.Longitude,
                    TripId = d.TripId
                }).ToList()
            };

            return tripDetailsRequestCloned;
        }

        private async void SetCurrentLocationAsync()
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

            CurrentLocation locationRequest = new CurrentLocation
            {
                UserId = User.Id,
                CurrentLocationLatLng = new LatLng { lat = _geolocatorService.Latitude, lng = _geolocatorService.Longitude },
                CurrentLocationAddress = Source
            };

            Response1<bool> response = await _apiService.SetCurrentLocation(Constants.urlBase, Constants.servicePrefix, Constants.SetCurrentLocationAppSRAsyncController, locationRequest, Constants.tokenType, token.Result.token, User.Id);

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
