using ClubersDeliveryMobile.Prism.Helpers;
using ClubersDeliveryMobile.Prism.Models;
using ClubersDeliveryMobile.Prism.Services;
using Newtonsoft.Json;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ClubersDeliveryMobile.Prism.ViewModels
{
    public class HistoryPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private List<DeliveryInfo> _deliveriesInfo;
        private readonly IApiService _apiService;
        private ObservableCollection<Grouping<DateTime, DeliveryInfo>> _deliveriesGrouped;
        private string _checkIcon = IconFont.CheckCircle;

        public HistoryPageViewModel(INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Entregas"; 
        }
        public string CheckIcon
        {
            get => _checkIcon;
            set => SetProperty(ref _checkIcon, value);
        }
        public ObservableCollection<Grouping<DateTime, DeliveryInfo>> DeliveriesGrouped
        {
            get => _deliveriesGrouped;
            set => SetProperty(ref _deliveriesGrouped, value);
        }
        public List<DeliveryInfo> DeliveriesInfo
        {
            get => _deliveriesInfo;
            set => SetProperty(ref _deliveriesInfo, value);
        }

        private async void LoadDeliveriesInfo()
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

            Response1<List<DeliveryInfo>> response = await _apiService.GetDeliveries(Constants.urlBase, Constants.servicePrefix, Constants.GetDeliveriesSRAsyncController, Constants.tokenType, token.Result.token, User.Id);

            if (response.ResultCode != ResultCode.Success)
            {
                await App.Current.MainPage.DisplayAlert(Constants.ErrorMessage, response.ResultMessages.FirstOrDefault(), Constants.AcceptMessage);
                return;
            }

            DeliveriesInfo = response.Result;

            var sorted = from Deliv in DeliveriesInfo
                         orderby Deliv.StartDate.Date descending
                         group Deliv by Deliv.StartDate.Date into DelivGroup
                         select new Grouping<DateTime, DeliveryInfo>(DelivGroup.Key, DelivGroup);

            DeliveriesGrouped = new ObservableCollection<Grouping<DateTime, DeliveryInfo>>(sorted);

            IsRunning = false;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            LoadDeliveriesInfo();
        }
    }
}
