using ClubersDeliveryMobile.Prism.Models;
using Xamarin.Forms;

namespace ClubersDeliveryMobile.Prism.Views
{
    public partial class ReportProblemPage : ContentPage
    {
        public ReportProblemPage()
        {
            InitializeComponent();
            PhoneNumberIcon.Text = IconFont.Phone;
            AlertCircleIcon1.Text = IconFont.AlertCircleOutline;
            AlertCircleIcon2.Text = IconFont.AlertCircleOutline;
            AlertCircleIcon3.Text = IconFont.AlertCircleOutline;
            AlertCircleIcon4.Text = IconFont.AlertCircleOutline;
            AlertCircleIcon5.Text = IconFont.AlertCircleOutline;
        }
    }
}
