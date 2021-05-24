using ClubersDeliveryMobile.Prism.Helpers;
using ClubersDeliveryMobile.Prism.Models;
using ClubersDeliveryMobile.Prism.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ClubersDeliveryMobile.Prism.ViewModels
{
    public class EarningsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private List<EarningsInfo> _earningsInfo;
        private readonly IApiService _apiService;
        private ObservableCollection<Grouping<DateTime, EarningsInfo>> _earningsGrouped;
        private DateTime _startDate;
        private DateTime _endDate;
        private DelegateCommand _filterCommand;

        public ICommand ExpandCommand { get; private set; }
        public bool IsExpanded { get; set; }
        public string Message { get; private set; }

        public EarningsPageViewModel(INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Ganancias";

            IsExpanded = true;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            LoadTransactionsAsync();

        }
        public DelegateCommand FilterCommand => _filterCommand ?? (_filterCommand = new DelegateCommand(FiltersAsync));
        public DateTime StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }
        public DateTime EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }
        public ObservableCollection<Grouping<DateTime, EarningsInfo>> EarningsGrouped
        {
            get => _earningsGrouped;
            set => SetProperty(ref _earningsGrouped, value);
        }

        public List<EarningsInfo> EarningsInfo
        {
            get => _earningsInfo;
            set => SetProperty(ref _earningsInfo, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            LoadTransactionsAsync();
        }

        private async void LoadTransactionsAsync()
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

            Response1<List<EarningsInfo>> response = await _apiService.GetEarnings(Constants.urlBase, Constants.servicePrefix, Constants.GetEarningsSRAsyncController, Constants.tokenType, token.Result.token, User.Id);

            if (response.ResultCode != ResultCode.Success)
            {
                await App.Current.MainPage.DisplayAlert(Constants.ErrorMessage, response.ResultMessages.FirstOrDefault(), Constants.AcceptMessage);
                return;
            }

            EarningsInfo = response.Result;

            var sorted = from Earn in EarningsInfo
                         orderby Earn.Created.Date descending
                         group Earn by Earn.Created.Date into EarnGroup
                         select new Grouping<DateTime, EarningsInfo>(EarnGroup.Key, EarnGroup);

            EarningsGrouped = new ObservableCollection<Grouping<DateTime, EarningsInfo>>(sorted);

            IsRunning = false;
        }

        private async void FiltersAsync()
        {
            LoadTransactionsAsync();
            EarningsGrouped = new ObservableCollection<Grouping<DateTime, EarningsInfo>>(_earningsGrouped.Where(t => (t.Key.Date >= StartDate.Date && t.Key.Date <= EndDate.Date)));
        }
    }
}
