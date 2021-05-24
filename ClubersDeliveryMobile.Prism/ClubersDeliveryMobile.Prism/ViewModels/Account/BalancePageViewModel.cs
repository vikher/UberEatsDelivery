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
    public class BalancePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private List<AccountStatus> _transactions;
        private ObservableCollection<Grouping<DateTime, AccountStatus>> _transactionsGrouped;
        private readonly IApiService _apiService;
        public BalancePageViewModel(INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Estado de cuenta";
            LoadTransactionsAsync();
        }
        public ObservableCollection<Grouping<DateTime, AccountStatus>> TransactionsGrouped
        {
            get => _transactionsGrouped;
            set => SetProperty(ref _transactionsGrouped, value);
        }

        public List<AccountStatus> Transactions
        {
            get => _transactions;
            set => SetProperty(ref _transactions, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            
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

            Response1<List<AccountStatus>> response = await _apiService.GetAllTransactionsAsync(Constants.urlBase, Constants.servicePrefix, Constants.GetAccountStatusAppSRAsyncController, Constants.tokenType, token.Result.token, User.Id);

            if (response.ResultCode != ResultCode.Success)
            {
                await App.Current.MainPage.DisplayAlert(Constants.ErrorMessage, response.ResultMessages.FirstOrDefault(), Constants.AcceptMessage);
                return;
            }

            Transactions = response.Result;

            var sorted = from Trans in Transactions
                         orderby Trans.DateOfTransaction
                         group Trans by Trans.DateOfTransaction into TransGroup
                         select new Grouping<DateTime, AccountStatus>(TransGroup.Key, TransGroup);

            TransactionsGrouped = new ObservableCollection<Grouping<DateTime, AccountStatus>>(sorted);

            IsRunning = false;
        }

    }
}
