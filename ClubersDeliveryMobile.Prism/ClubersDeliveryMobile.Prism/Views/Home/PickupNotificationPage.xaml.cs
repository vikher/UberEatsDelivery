using Xamarin.Forms;
using Rg.Plugins.Popup.Pages;

namespace ClubersDeliveryMobile.Prism.Views.Home
{
    public partial class PickupNotificationPage : PopupPage
    {
        public PickupNotificationPage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return base.OnBackButtonPressed();
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return base.OnBackgroundClicked();
        }
    }
}