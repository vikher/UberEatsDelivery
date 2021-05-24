using ClubersDeliveryMobile.Prism.Models;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace ClubersDeliveryMobile.Prism.ViewModels
{
    public class SupportPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private List<Problem> _problems;
        private bool _isEnabled;
        private Problem _selectedProblem;
        private DelegateCommand _whatsappCommand;
        private DelegateCommand _selectedProblemCommand;

        public SupportPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Soporte";
            Problems = new List<Problem>()
                {
                    new Problem(){Description = "Tengo un problema con un pedido"},
                    new Problem(){Description = "Tengo un problema con un Socio Consumidor"},
                    new Problem(){Description = "Tengo un problema con un cobro"},
                    new Problem(){Description = "Tengo un problema con un Saldo"},
                };
        }
        public DelegateCommand WhatsappCommand => _whatsappCommand ?? (_whatsappCommand = new DelegateCommand(WhatsappAsync));
        public DelegateCommand SelectedProblemCommand => _selectedProblemCommand ?? (_selectedProblemCommand = new DelegateCommand(SelectedProblemAsync));

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }
        public Problem SelectedProblem
        {
            get => _selectedProblem;
            set => SetProperty(ref _selectedProblem, value);
        }
        public List<Problem> Problems
        {
            get => _problems;
            set => SetProperty(ref _problems, value);
        }

        private async void WhatsappAsync()
        {
            if (SelectedProblem.Description == "Tengo un problema con un pedido")
            {
                await Launcher.TryOpenAsync(Constants.wa1);
            }
            else if (SelectedProblem.Description == "Tengo un problema con un Socio Consumidor")
            {
                await Launcher.TryOpenAsync(Constants.wa2);
            }
            else if (SelectedProblem.Description == "Tengo un problema con un cobro")
            {
                await Launcher.TryOpenAsync(Constants.wa3);
            }
            else if (SelectedProblem.Description == "Tengo un problema con un Saldo")
            {
                await Launcher.TryOpenAsync(Constants.wa4);
            }
            else
            {
                return;
            }
        }

        private void SelectedProblemAsync()
        {
            IsEnabled = true;
        }
    }
}