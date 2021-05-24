using ClubersDeliveryMobile.Prism.Helpers;
using ClubersDeliveryMobile.Prism.Models;
using ClubersDeliveryMobile.Prism.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClubersDeliveryMobile.Prism.ViewModels
{
    public class ChargesPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private List<Transaction> _chargeTransactions;
        private List<Transaction> _paidChargeTransactions;
        private List<Transaction> _nonPaidChargeTransactions;
        private List<PaymentCash> _paymentCash;

        private int _paidChargeTransactionsTotal;
        private int _nonPaidChargeTransactionsTotal;
        private double _paymentCashTotal;

        
        private DateTime _startDate;
        private DateTime _endDate;
        private DelegateCommand _refreshViewCommand;
        private DelegateCommand _goToPaymentConfirmationCommand;
        private DelegateCommand _filterCommand;

        private readonly IApiService _apiService;
        public ChargesPageViewModel(INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Cobros efectivo";
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            LoadPaymentCashAsync();

        }
        public DelegateCommand GoToPaymentConfirmationCommand => _goToPaymentConfirmationCommand ?? (_goToPaymentConfirmationCommand = new DelegateCommand(GoToPaymentConfirmationAsync));
        public DelegateCommand RefreshViewCommand => _refreshViewCommand ?? (_refreshViewCommand = new DelegateCommand(RefreshData));

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
        private void RefreshData()
        {
            IsRefreshing = false;
        }
        private async void GoToPaymentConfirmationAsync()
        {
            await _navigationService.NavigateAsync("PaymentConfirmationPage");
        }

        public int PaidChargeTransactionsTotal
        {
            get => _paidChargeTransactionsTotal;
            set => SetProperty(ref _paidChargeTransactionsTotal, value);
        }
        public int NonPaidChargeTransactionsTotal
        {
            get => _nonPaidChargeTransactionsTotal;
            set => SetProperty(ref _nonPaidChargeTransactionsTotal, value);
        }

        public double PaymentCashTotal
        {
            get => _paymentCashTotal;
            set => SetProperty(ref _paymentCashTotal, value);
        }

        public List<PaymentCash> PaymentCash
        {
            get => _paymentCash;
            set => SetProperty(ref _paymentCash, value);
        }
        public List<Transaction> ChargeTransactions
        {
            get => _chargeTransactions;
            set => SetProperty(ref _chargeTransactions, value);
        }
        public List<Transaction> PaidChargeTransactions
        {
            get => _paidChargeTransactions;
            set => SetProperty(ref _paidChargeTransactions, value);
        }
        public List<Transaction> NonPaidChargeTransactions
        {
            get => _nonPaidChargeTransactions;
            set => SetProperty(ref _nonPaidChargeTransactions, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            LoadPaymentCashAsync();
        }

        private async void LoadPaymentCashAsync()
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

            Response1<List<PaymentCash>> response = await _apiService.GetPaymentCash(Constants.urlBase, Constants.servicePrefix, Constants.GetPaymentsCashAppSRAsyncController, Constants.tokenType, token.Result.token, User.Id);

            if (response.ResultCode != ResultCode.Success)
            {
                await App.Current.MainPage.DisplayAlert(Constants.ErrorMessage, response.ResultMessages.FirstOrDefault(), Constants.AcceptMessage);
                return;
            }

            PaymentCash = response.Result;

            PaymentCashTotal = PaymentCash.Sum(item => item.Amount);

            IsRunning = false;
            
        }
    }
}
