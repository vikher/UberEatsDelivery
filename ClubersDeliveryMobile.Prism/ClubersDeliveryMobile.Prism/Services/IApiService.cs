using ClubersDeliveryMobile.Prism.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClubersDeliveryMobile.Prism.Services
{
    public interface IApiService
    {
        bool CheckConnection();
        Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request);
        Task<RecoverPasswordResponse> RecoverPasswordAsync(string urlBase, string servicePrefix, string controller, EmailRequest emailRequest);
        Task<Response> GetUserById(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken);
        Task<Response1<List<AccountStatus>>> GetAllTransactionsAsync(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, int id);
        Task<Response1<Order>> GetOrderDetails(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, int id);
        Task<Response1<ProfileInfo>> GetProfile(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, int id);
        Task<Response1<UserResponse>> GetUserByEmailAsync(string urlBase, string servicePrefix, string controller, string email, string tokenType, string accessToken);
        Task<Response1<List<DeliveryInfo>>> GetDeliveries(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, int id);
        Task<Response1<List<EarningsInfo>>> GetEarnings(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, int id);
        Task<Response1<List<PaymentCash>>> GetPaymentCash(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, int id);
        Task<Response1<DeliveryMenStatus>> GetStatusDeliveryMen(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, int id);
        Task<Response> NewTripAsync(string urlBase, string servicePrefix, string controller, TripRequest model, string tokenType, string accessToken);
        Task<Response> AddTripDetailsAsync(string urlBase, string servicePrefix, string controller, TripDetailsRequest model, string tokenType, string accessToken);
        Task<Response1<bool>> SetCurrentLocation<T>(string urlBase, string servicePrefix, string controller, T model, string tokenType, string accessToken, int id);
        Task<Response1<bool>> AcceptOrder(string urlBase, string servicePrefix, string controller, string orderId, string tokenType, string accessToken, int id);
        Task<Response1<bool>> RejectOrder(string urlBase, string servicePrefix, string controller, string orderId, string tokenType, string accessToken, int id);
        Task<Response1<bool>> UpdateIsAvailable(string urlBase, string servicePrefix, string controller, bool isAvailable, string tokenType, string accessToken, int id);
        Task<Response1<bool>> FinalizePaidOrder(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, int orderId);
        Task<Response1<bool>> FinalizeUnPaidOrder(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, int orderId);


    }
}


