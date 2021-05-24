using ClubersDeliveryMobile.Prism.Helpers;
using ClubersDeliveryMobile.Prism.Models;
using ClubersDeliveryMobile.Prism.Services;
using ClubersDeliveryMobile.Prism.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System.Linq;

namespace ClubersDeliveryMobile.Prism.ViewModels
{
    public class ProfilePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private DelegateCommand _goToHistoryCommand;
        private UserResponse _user;
        private ProfileInfo _profileInfo;

        private string _checkIcon = IconFont.CheckCircleOutline;
        public ProfilePageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Perfil";
            LoadUser();
        }
        public DelegateCommand GoToHistoryCommand => _goToHistoryCommand ?? (_goToHistoryCommand = new DelegateCommand(GoToHistoryAsync));
        public string CheckIcon
        {
            get => _checkIcon;
            set => SetProperty(ref _checkIcon, value);
        }
        public UserResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public ProfileInfo ProfileInfo
        {
            get => _profileInfo;
            set => SetProperty(ref _profileInfo, value);
        }
        private void LoadUser()
        {
            if (Settings.IsLogin)
            {
                User = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            }
        }

        private async void LoadProfileInfo()
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

            Response1<ProfileInfo> response = await _apiService.GetProfile(Constants.urlBase, Constants.servicePrefix, Constants.GetProfileSRByIdAsyncController, Constants.tokenType, token.Result.token, User.Id);

            if (response.ResultCode != ResultCode.Success)
            {
                await App.Current.MainPage.DisplayAlert(Constants.ErrorMessage, response.ResultMessages.FirstOrDefault(), Constants.AcceptMessage);
                return;
            }

            ProfileInfo = response.Result;

            IsRunning = false;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            LoadProfileInfo();
        }
        private async void GoToHistoryAsync()
        {
            await _navigationService.NavigateAsync($"/{nameof(DeliveryMasterDetailPage)}/NavigationPage/{nameof(HistoryPage)}");
        }
    }
}
