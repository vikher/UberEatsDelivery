using Prism;
using Prism.Ioc;
using ClubersDeliveryMobile.Prism.ViewModels;
using ClubersDeliveryMobile.Prism.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ClubersDeliveryMobile.Prism.Helpers;
using ClubersDeliveryMobile.Prism.Services;
using ClubersDeliveryMobile.Prism.Interfaces;
using Prism.Plugin.Popups;
using ClubersDeliveryMobile.Prism.Views.Home;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ClubersDeliveryMobile.Prism
{
    public partial class App
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            Device.SetFlags(new[] {
                "Shapes_Experimental"
            });

            if (Settings.IsRemembered && Settings.IsLogin)
            {
                await NavigationService.NavigateAsync($"/{nameof(DeliveryMasterDetailPage)}/NavigationPage/{nameof(HomePage)}");
            }
            else
            {
                await NavigationService.NavigateAsync($"/NavigationPage/{nameof(LoginPage)}");
            }


        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterPopupNavigationService();

            containerRegistry.Register<IGeolocatorService, GeolocatorService>();
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.Register<IRegexHelper, RegexHelper>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<ProfilePage, ProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<DeliveryMasterDetailPage, DeliveryMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<AccountTabbedPage, AccountTabbedPageViewModel>();
            containerRegistry.RegisterForNavigation<EarningsPage, EarningsPageViewModel>();
            containerRegistry.RegisterForNavigation<ChargesPage, ChargesPageViewModel>();
            containerRegistry.RegisterForNavigation<BalancePage, BalancePageViewModel>();
            containerRegistry.RegisterForNavigation<HistoryPage, HistoryPageViewModel>();
            containerRegistry.RegisterForNavigation<StartDeliveryPage, StartDeliveryPageViewModel>();
            containerRegistry.RegisterForNavigation<EndDeliveryPage, EndDeliveryPageViewModel>();
            containerRegistry.RegisterForNavigation<DeliveryProblemPage, DeliveryProblemPageViewModel>();
            containerRegistry.RegisterForNavigation<ReportProblemPage, ReportProblemPageViewModel>();
            containerRegistry.RegisterForNavigation<PaymentConfirmationPage, PaymentConfirmationPageViewModel>();
            containerRegistry.RegisterForNavigation<SupportPage, SupportPageViewModel>();
            containerRegistry.RegisterForNavigation<RecoverPasswordPage, RecoverPasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<UpdatePasswordPage, UpdatePasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<RecoverPasswordPage, RecoverPasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<PickupNotificationPage, PickupNotificationPageViewModel>();
            containerRegistry.RegisterForNavigation<PickupDetailsPage, PickupDetailsPageViewModel>();
            containerRegistry.RegisterForNavigation<MessagesPage, MessagesPageViewModel>();
            containerRegistry.RegisterForNavigation<PaidDeliveryPage, PaidDeliveryPageViewModel>();
            containerRegistry.RegisterForNavigation<NonPaidDeliveryPage, NonPaidDeliveryPageViewModel>();
        }
    }
}
