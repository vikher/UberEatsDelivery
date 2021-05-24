using Xamarin.Forms;

namespace ClubersDeliveryMobile.Prism.Views
{
    public partial class DeliveryMasterDetailPage : MasterDetailPage
    {
        public static DeliveryMasterDetailPage Current { get; set; }
        public DeliveryMasterDetailPage()
        {
            InitializeComponent();

            Current = this;
        }
    }
}