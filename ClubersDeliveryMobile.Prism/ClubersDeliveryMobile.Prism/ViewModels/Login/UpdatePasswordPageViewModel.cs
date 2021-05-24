using ClubersDeliveryMobile.Prism.Services;
using ClubersDeliveryMobile.Prism.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Threading.Tasks;

namespace ClubersDeliveryMobile.Prism.ViewModels
{
    public class UpdatePasswordPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;
        private DelegateCommand _updatePasswordCommand;

        private string _password;
        private string _passwordConfirm;

        public UpdatePasswordPageViewModel(INavigationService navigationService,
                                         IPageDialogService dialogService,
                                         IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _apiService = apiService;
            Title = "Actualizar contraseña";
        }
        public DelegateCommand UpdatePasswordCommand => _updatePasswordCommand ?? (_updatePasswordCommand = new DelegateCommand(UpdatePasswordAsync));

        public string Email { get; set; }
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        public string PasswordConfirm
        {
            get => _passwordConfirm;
            set => SetProperty(ref _passwordConfirm, value);
        }


        private async Task<bool> ValidateDataAsync()
        {

            if (string.IsNullOrEmpty(Password))
            {
                await _dialogService.DisplayAlertAsync(Constants.ErrorMessage, "Debe ingresar una contraseña.", Constants.AcceptMessage);
                return false;
            }

            if (Password.Length < 8)
            {
                await _dialogService.DisplayAlertAsync(Constants.ErrorMessage, "La contraseña debe contener al menos 8 caracteres.", Constants.AcceptMessage);
                return false;
            }

            if (string.IsNullOrEmpty(PasswordConfirm))
            {
                await _dialogService.DisplayAlertAsync(Constants.ErrorMessage, "Debe confirmar la contraseña.", Constants.AcceptMessage);
                return false;
            }

            if (!Password.Equals(PasswordConfirm))
            {
                await _dialogService.DisplayAlertAsync(Constants.ErrorMessage, "La contraseña y la confirmación no coinciden.", Constants.AcceptMessage);
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
                await _dialogService.DisplayAlertAsync(Constants.ErrorMessage, Constants.ConnectionError, Constants.AcceptMessage);
                return;
            }

            IsRunning = false;

            await _dialogService.DisplayAlertAsync(Constants.ErrorMessage, "Contraseña Actualizada con Exito", Constants.AcceptMessage);
            await NavigationService.NavigateAsync($"/NavigationPage/{nameof(LoginPage)}");


        }
    }
}
