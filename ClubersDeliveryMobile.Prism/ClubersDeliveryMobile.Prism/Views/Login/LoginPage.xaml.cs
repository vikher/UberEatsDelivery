using Xamarin.Essentials;
using Xamarin.Forms;

namespace ClubersDeliveryMobile.Prism.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            var currentVersion = VersionTracking.CurrentVersion;
            var currentBuild = VersionTracking.CurrentBuild;
            VersionLabel.Text = $"Build {currentBuild} Version ( {currentVersion})";

        }

        private void Email_Focused(object sender, FocusEventArgs e)
        {
            boxEmail.BorderColor = Color.DeepPink;
        }

        private void Password_Focused(object sender, FocusEventArgs e)
        {
            boxPassword.BorderColor = Color.DeepPink;
        }

        private void box1_Unfocused(object sender, FocusEventArgs e)
        {
            boxEmail.BorderColor = Color.Gray;
            boxPassword.BorderColor = Color.Gray;

        }
    }
}
