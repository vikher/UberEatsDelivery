using System.Threading.Tasks;

namespace ClubersDeliveryMobile.Prism.Services
{
    public interface IGeolocatorService
    {
        double Latitude { get; set; }

        double Longitude { get; set; }

        Task GetLocationAsync();
    }
}