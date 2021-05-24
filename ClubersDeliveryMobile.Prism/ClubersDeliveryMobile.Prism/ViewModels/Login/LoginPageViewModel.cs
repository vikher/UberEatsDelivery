using ClubersDeliveryMobile.Prism.Helpers;
using ClubersDeliveryMobile.Prism.Interfaces;
using ClubersDeliveryMobile.Prism.Models;
using ClubersDeliveryMobile.Prism.Services;
using ClubersDeliveryMobile.Prism.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Linq;
using Xamarin.Essentials;

namespace ClubersDeliveryMobile.Prism.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IRegexHelper _regexHelper;
        private readonly IApiService _apiService;
        private bool _isEnabled;
        private string _password;
        private bool _isRemember;
        private string _emailIcon = IconFont.Email;
        private string _passwordIcon = IconFont.Lock;
        private UserResponse _userResponse;
        private DelegateCommand _loginCommand;
        private DelegateCommand _registerCommand;
        private DelegateCommand _forgotPasswordCommand;
        public LoginPageViewModel(INavigationService navigationService, IApiService apiService, IRegexHelper regexHelper, IPageDialogService dialogService) : base(navigationService)
        {
            _apiService = apiService;
            _navigationService = navigationService;
            _dialogService = dialogService;
            _regexHelper = regexHelper;
            Title = "Inicio";
            IsRemember = true;
        }
        public DelegateCommand ForgotPasswordCommand => _forgotPasswordCommand ?? (_forgotPasswordCommand = new DelegateCommand(ForgotPasswordAsync));
        public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(LoginAsync));
        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }
        public string EmailIcon
        {
            get => _emailIcon;
            set => SetProperty(ref _emailIcon, value);
        }

        public string PasswordIcon
        {
            get => _passwordIcon;
            set => SetProperty(ref _passwordIcon, value);
        }
        public string Email { get; set; }
        public bool IsRemember
        {
            get => _isRemember;
            set => SetProperty(ref _isRemember, value);
        }
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public UserResponse UserResponse
        {
            get => _userResponse;
            set => SetProperty(ref _userResponse, value);
        }
        private async void LoginAsync()
        {
            if (string.IsNullOrEmpty(Email) || !_regexHelper.IsValidEmail(Email))
            {
                await _dialogService.DisplayAlertAsync(Constants.ErrorMessage, "Debe ingresar un correo.", Constants.AcceptMessage);
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await _dialogService.DisplayAlertAsync(Constants.ErrorMessage, "Debe ingresar una contraseña.", Constants.AcceptMessage);
                return;
            }

            if (Password.Length < 8)
            {
                await _dialogService.DisplayAlertAsync(Constants.ErrorMessage, "La contraseña debe contener al menos 6 caracteres", Constants.AcceptMessage);
                return;
            }


            IsRunning = true;
            IsEnabled = false;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Constants.ErrorMessage, "Compruebe la conexión a Internet", Constants.AcceptMessage);
                return;
            }

            TokenRequest request = new TokenRequest
            {
                password = Password,
                email = Email
            };

            Response response = await _apiService.GetTokenAsync(Constants.urlBase, Constants.servicePrefix, Constants.GetTokenController, request);

            if (response.ResultCode != ResultCode.Success)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Constants.ErrorMessage, "Email o contraseña incorrectos", Constants.AcceptMessage);
                Password = string.Empty;
                return;
            }

            TokenResponse token = (TokenResponse)response.Result;

            Response1<UserResponse> response2 = await _apiService.GetUserByEmailAsync(Constants.urlBase, Constants.servicePrefix, Constants.GetUserByEmailAsyncController, Email, Constants.tokenType, token.Result.token);

            if (response2.ResultCode != ResultCode.Success)
            {
                await App.Current.MainPage.DisplayAlert(Constants.ErrorMessage, response.ResultMessages.FirstOrDefault(), Constants.AcceptMessage);
                return;
            }

            UserResponse = response2.Result;

            Settings.User = JsonConvert.SerializeObject(UserResponse);
            Settings.Token = JsonConvert.SerializeObject(token);
            Settings.IsLogin = true;

            IsRunning = false;
            IsEnabled = true;

            await _navigationService.NavigateAsync($"/{nameof(DeliveryMasterDetailPage)}/NavigationPage/{nameof(HomePage)}");
            Settings.IsRemembered = IsRemember;
        }
        private async void ForgotPasswordAsync()
        {
            await _navigationService.NavigateAsync(nameof(RecoverPasswordPage));
        }
    }
}
