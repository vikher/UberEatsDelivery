using ClubersDeliveryMobile.Prism.Interfaces;
using ClubersDeliveryMobile.Prism.Models;
using ClubersDeliveryMobile.Prism.Services;
using ClubersDeliveryMobile.Prism.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Linq;
using System.Threading.Tasks;

namespace ClubersDeliveryMobile.Prism.ViewModels
{
    public class RecoverPasswordPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IRegexHelper _regexHelper;
        private bool _isEnabled;

        private readonly IApiService _apiService;
        private DelegateCommand _recoverCommand;
        public RecoverPasswordPageViewModel(INavigationService navigationService,
                                         IRegexHelper regexHelper,
                                         IPageDialogService dialogService,
                                         IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _regexHelper = regexHelper;
            _apiService = apiService;
            Title = "Recuperar Contraseña";
            IsEnabled = true;
        }
        public DelegateCommand RecoverCommand => _recoverCommand ?? (_recoverCommand = new DelegateCommand(RecoverAsync));
        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }
        public string Email { get; set; }

        private async Task<bool> ValidateDataAsync()
        {

            if (string.IsNullOrEmpty(Email) || !_regexHelper.IsValidEmail(Email))
            {
                await _dialogService.DisplayAlertAsync(Constants.ErrorMessage, "Debe ingresar un correo.", Constants.AcceptMessage);
                return false;
            }
            return true;
        }
        private async void UpdatePasswordAsync()
        {

            bool isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }

            IsRunning = true;
            if (!_apiService.CheckConnection())
            {
                IsRunning = false;
                await _dialogService.DisplayAlertAsync(Constants.ErrorMessage, "Error.", Constants.AcceptMessage);
                return;
            }

            IsRunning = false;

            await _navigationService.NavigateAsync(nameof(UpdatePasswordPage));
        }
        private async void RecoverAsync()
        {

            bool isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            if (!_apiService.CheckConnection())
            {
                IsRunning = false;
                IsEnabled = true;
                await _dialogService.DisplayAlertAsync(Constants.ErrorMessage, "Compruebe la conexión a Internet", Constants.AcceptMessage);
                return;
            }



            EmailRequest request = new EmailRequest
            {
                email = Email,
            };

            RecoverPasswordResponse response = await _apiService.RecoverPasswordAsync(Constants.urlBase, Constants.servicePrefix, Constants.ResetPasswordController, request);

            IsRunning = false;
            IsEnabled = true;

            if (!response.result)
            {
                await App.Current.MainPage.DisplayAlert(Constants.ErrorMessage, response.ResultMessages.FirstOrDefault(), Constants.AcceptMessage);
                return;
            }

            await App.Current.MainPage.DisplayAlert(Constants.ErrorMessage, response.ResultMessages.FirstOrDefault(), Constants.AcceptMessage);
            await _navigationService.GoBackAsync();
        }
    }
}
