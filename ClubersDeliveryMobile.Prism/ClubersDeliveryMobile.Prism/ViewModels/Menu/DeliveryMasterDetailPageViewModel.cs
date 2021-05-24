using ClubersDeliveryMobile.Prism.Helpers;
using ClubersDeliveryMobile.Prism.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ClubersDeliveryMobile.Prism.ViewModels
{
    public class DeliveryMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private UserResponse _user;

        public DeliveryMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
            LoadUser();
        }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }
        public UserResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }
        private void LoadUser()
        {
            if (Settings.IsLogin)
            {
                User = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            }
        }
        private void LoadMenus()
        {
            if (Settings.IsDeliveryStarted)
            {
                List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    Icon = IconFont.MapMarker,
                    PageName = "PickupDetailsPage",
                    Title = "Viaje"
                },

                new Menu
                {
                    Icon = IconFont.Account,
                    PageName = "ProfilePage",
                    Title = "Perfil"
                },

                new Menu
                {
                    Icon = IconFont.Cash,
                    PageName = "AccountTabbedPage?selectedTab=EarningsPage",
                    Title = "Cuenta"
                },
                new Menu
                {
                    Icon = IconFont.Shopping,
                    PageName = "HistoryPage",
                    Title = "Entregas"
                },
                new Menu
                {
                    Icon = IconFont.Headset,
                    PageName = "SupportPage",
                    Title = "Soporte"
                },
                new Menu
                {
                    Icon = IconFont.Logout,
                    PageName = "LoginPage",
                    Title = "Cerrar sesión"
                }
            };
                Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title
                }).ToList());
            }
            else
            {
                List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    Icon = IconFont.MapMarker,
                    PageName = "HomePage",
                    Title = "Viaje"
                },

                new Menu
                {
                    Icon = IconFont.Account,
                    PageName = "ProfilePage",
                    Title = "Perfil"
                },

                new Menu
                {
                    Icon = IconFont.Cash,
                    PageName = "AccountTabbedPage?selectedTab=EarningsPage",
                    Title = "Cuenta"
                },
                new Menu
                {
                    Icon = IconFont.Shopping,
                    PageName = "HistoryPage",
                    Title = "Entregas"
                },
                new Menu
                {
                    Icon = IconFont.Headset,
                    PageName = "SupportPage",
                    Title = "Soporte"
                },
                new Menu
                {
                    Icon = IconFont.Logout,
                    PageName = "LoginPage",
                    Title = "Cerrar sesión"
                }
            };
                Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title
                }).ToList());
            }
           
        }
    }
}
