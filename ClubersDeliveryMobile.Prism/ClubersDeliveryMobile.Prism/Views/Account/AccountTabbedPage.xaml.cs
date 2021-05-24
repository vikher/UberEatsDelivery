using Xamarin.Forms;

namespace ClubersDeliveryMobile.Prism.Views
{
    public partial class AccountTabbedPage : TabbedPage
    {
        public AccountTabbedPage()
        {
            InitializeComponent();
            UnselectedTabColor = Color.WhiteSmoke;
            SelectedTabColor = Color.White;
            BarBackgroundColor = (Color)Application.Current.Resources["cluberspink"];
        }
    }
}
