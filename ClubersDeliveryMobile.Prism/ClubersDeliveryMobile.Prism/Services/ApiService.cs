using ClubersDeliveryMobile.Prism.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ClubersDeliveryMobile.Prism.Services
{
    public class ApiService : IApiService
    {
        public ApiService()
        {
        }

        public bool CheckConnection()
        {
            return Connectivity.NetworkAccess == NetworkAccess.Internet;
        }

        public async Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request)
        {
            try
            {
                string requestString = JsonConvert.SerializeObject(request);
                StringContent content = new StringContent(requestString, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                string url = $"{servicePrefix}{controller}";
                HttpResponseMessage response = await client.PostAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();

                Response ResponseResult = JsonConvert.DeserializeObject<Response>(result);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(result);
                return new Response
                {
                    ResultCode = ResultCode.Success,
                    //IsSuccess = true,
                    Result = token
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }

        public async Task<Response> GetUserById(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}/{1}";
                HttpResponseMessage response = await client.GetAsync(url);
                string result = await response.Content.ReadAsStringAsync();

                Response ResponseResult = JsonConvert.DeserializeObject<Response>(result);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                UserResponse userResponse = JsonConvert.DeserializeObject<UserResponse>(result);


                return new Response
                {
                    ResultCode = ResultCode.Success,
                    Result = userResponse
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }

        public async Task<RecoverPasswordResponse> RecoverPasswordAsync(string urlBase, string servicePrefix, string controller, EmailRequest emailRequest)
        {
            try
            {
                string request = JsonConvert.SerializeObject(emailRequest);
                StringContent content = new StringContent(request, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                string url = $"{servicePrefix}{controller}";
                HttpResponseMessage response = await client.PostAsync(url, content);
                string answer = await response.Content.ReadAsStringAsync();
                RecoverPasswordResponse obj = JsonConvert.DeserializeObject<RecoverPasswordResponse>(answer);
                return obj;
            }
            catch (Exception ex)
            {
                return new RecoverPasswordResponse
                {
                    result = false,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }

        public async Task<Response1<List<AccountStatus>>> GetAllTransactionsAsync(string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken, 
            int id)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}/{id}";
                HttpResponseMessage response = await client.GetAsync(url);
                string answer = await response.Content.ReadAsStringAsync();

                Response1<List<AccountStatus>> ResponseResult = JsonConvert.DeserializeObject<Response1<List<AccountStatus>>>(answer);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response1<List<AccountStatus>>
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                return new Response1<List<AccountStatus>>
                {
                    ResultCode = ResultCode.Success,
                    Result = ResponseResult.Result
                };
            }
            catch (Exception ex)
            {
                return new Response1<List<AccountStatus>>
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }
        public async Task<Response1<UserResponse>> GetUserByEmailAsync(string urlBase, string servicePrefix, string controller, string email, string tokenType, string accessToken)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}/{email}";
                HttpResponseMessage response = await client.GetAsync(url);
                string answer = await response.Content.ReadAsStringAsync();

                Response1<UserResponse> ResponseResult = JsonConvert.DeserializeObject<Response1<UserResponse>>(answer);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response1<UserResponse>
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                return new Response1<UserResponse>
                {
                    ResultCode = ResultCode.Success,
                    Result = ResponseResult.Result
                };
            }
            catch (Exception ex)
            {
                return new Response1<UserResponse>
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }

        public async Task<Response1<Order>> GetOrderDetails(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, int id)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}/{id}";
                HttpResponseMessage response = await client.GetAsync(url);
                string answer = await response.Content.ReadAsStringAsync();

                Response1<Order> ResponseResult = JsonConvert.DeserializeObject<Response1<Order>>(answer);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response1<Order>
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                return new Response1<Order>
                {
                    ResultCode = ResultCode.Success,
                    Result = ResponseResult.Result
                };
            }
            catch (Exception ex)
            {
                return new Response1<Order>
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }
        public async Task<Response1<ProfileInfo>> GetProfile(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, int id)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}/{id}";
                HttpResponseMessage response = await client.GetAsync(url);
                string answer = await response.Content.ReadAsStringAsync();

                Response1<ProfileInfo> ResponseResult = JsonConvert.DeserializeObject<Response1<ProfileInfo>>(answer);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response1<ProfileInfo>
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                return new Response1<ProfileInfo>
                {
                    ResultCode = ResultCode.Success,
                    Result = ResponseResult.Result
                };
            }
            catch (Exception ex)
            {
                return new Response1<ProfileInfo>
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }

        public async Task<Response1<DeliveryMenStatus>> GetStatusDeliveryMen(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, int id)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}/{id}";
                HttpResponseMessage response = await client.GetAsync(url);
                string answer = await response.Content.ReadAsStringAsync();

                Response1<DeliveryMenStatus> ResponseResult = JsonConvert.DeserializeObject<Response1<DeliveryMenStatus>>(answer);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response1<DeliveryMenStatus>
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                return new Response1<DeliveryMenStatus>
                {
                    ResultCode = ResultCode.Success,
                    Result = ResponseResult.Result
                };
            }
            catch (Exception ex)
            {
                return new Response1<DeliveryMenStatus>
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }

        public async Task<Response1<List<DeliveryInfo>>> GetDeliveries(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, int id)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}/{id}";
                HttpResponseMessage response = await client.GetAsync(url);
                string answer = await response.Content.ReadAsStringAsync();

                Response1<List<DeliveryInfo>> ResponseResult = JsonConvert.DeserializeObject<Response1<List<DeliveryInfo>>>(answer);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response1<List<DeliveryInfo>>
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                return new Response1<List<DeliveryInfo>>
                {
                    ResultCode = ResultCode.Success,
                    Result = ResponseResult.Result
                };
            }
            catch (Exception ex)
            {
                return new Response1<List<DeliveryInfo>>
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }

        public async Task<Response1<bool>> UpdateIsAvailable(string urlBase, string servicePrefix, string controller, bool isAvailable, string tokenType, string accessToken, int id)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}/{id}";
                HttpResponseMessage response = await client.PutAsync(url, null);
                string answer = await response.Content.ReadAsStringAsync();

                Response1<bool> ResponseResult = JsonConvert.DeserializeObject<Response1<bool>>(answer);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response1<bool>
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }
                return new Response1<bool>
                {
                    ResultCode = ResultCode.Success,
                    Result = ResponseResult.Result
                };
            }
            catch (Exception ex)
            {
                return new Response1<bool>
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }

        public async Task<Response1<List<EarningsInfo>>> GetEarnings(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, int id)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}/{id}";
                HttpResponseMessage response = await client.GetAsync(url);
                string answer = await response.Content.ReadAsStringAsync();

                Response1<List<EarningsInfo>> ResponseResult = JsonConvert.DeserializeObject<Response1<List<EarningsInfo>>>(answer);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response1<List<EarningsInfo>>
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                return new Response1<List<EarningsInfo>>
                {
                    ResultCode = ResultCode.Success,
                    Result = ResponseResult.Result
                };
            }
            catch (Exception ex)
            {
                return new Response1<List<EarningsInfo>>
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }

        public async Task<Response1<List<PaymentCash>>> GetPaymentCash(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, int id)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}/{id}";
                HttpResponseMessage response = await client.GetAsync(url);
                string answer = await response.Content.ReadAsStringAsync();

                Response1<List<PaymentCash>> ResponseResult = JsonConvert.DeserializeObject<Response1<List<PaymentCash>>>(answer);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response1<List<PaymentCash>>
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                return new Response1<List<PaymentCash>>
                {
                    ResultCode = ResultCode.Success,
                    Result = ResponseResult.Result
                };
            }
            catch (Exception ex)
            {
                return new Response1<List<PaymentCash>>
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }

        public async Task<Response> AddTripDetailsAsync(string urlBase, string servicePrefix, string controller, TripDetailsRequest model, string tokenType, string accessToken)
        {
            try
            {
                string request = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(request, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}";
                HttpResponseMessage response = await client.PostAsync(url, content);
                string answer = await response.Content.ReadAsStringAsync();

                Response1<List<PaymentCash>> ResponseResult = JsonConvert.DeserializeObject<Response1<List<PaymentCash>>>(answer);
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                return new Response
                {
                    ResultCode = ResultCode.Success
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }

        public async Task<Response> NewTripAsync(string urlBase, string servicePrefix, string controller, TripRequest model, string tokenType, string accessToken)
        {
            try
            {
                string request = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(request, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}";
                HttpResponseMessage response = await client.PostAsync(url, content);
                string answer = await response.Content.ReadAsStringAsync();

                Response1<List<PaymentCash>> ResponseResult = JsonConvert.DeserializeObject<Response1<List<PaymentCash>>>(answer);
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }

                TripResponse trip = JsonConvert.DeserializeObject<TripResponse>(answer);
                return new Response
                {
                    ResultCode = ResultCode.Success,
                    Result = ResponseResult.Result
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }

        public async Task<Response1<bool>> SetCurrentLocation<T>(string urlBase, string servicePrefix, string controller, T model, string tokenType, string accessToken, int id)
        {
            try
            {
                string request = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(request, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}/{id}";
                HttpResponseMessage response = await client.PutAsync(url, content);
                string answer = await response.Content.ReadAsStringAsync();

                Response1<bool> ResponseResult = JsonConvert.DeserializeObject<Response1<bool>>(answer);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response1<bool>
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }
                return new Response1<bool>
                {
                    ResultCode = ResultCode.Success,
                    Result = ResponseResult.Result
                };
            }
            catch (Exception ex)
            {
                return new Response1<bool>
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }
        public async Task<Response1<bool>> AcceptOrder(string urlBase, string servicePrefix, string controller, string orderId, string tokenType, string accessToken, int id)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}{orderId}/{id}";
                HttpResponseMessage response = await client.PostAsync(url, null);
                string answer = await response.Content.ReadAsStringAsync();

                Response1<bool> ResponseResult = JsonConvert.DeserializeObject<Response1<bool>>(answer);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response1<bool>
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }
                return new Response1<bool>
                {
                    ResultCode = ResultCode.Success,
                    Result = ResponseResult.Result
                };
            }
            catch (Exception ex)
            {
                return new Response1<bool>
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }
        public async Task<Response1<bool>> RejectOrder(string urlBase, string servicePrefix, string controller, string orderId, string tokenType, string accessToken, int id)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}{orderId}/{id}";
                HttpResponseMessage response = await client.PostAsync(url, null);
                string answer = await response.Content.ReadAsStringAsync();

                Response1<bool> ResponseResult = JsonConvert.DeserializeObject<Response1<bool>>(answer);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response1<bool>
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }
                return new Response1<bool>
                {
                    ResultCode = ResultCode.Success,
                    Result = ResponseResult.Result
                };
            }
            catch (Exception ex)
            {
                return new Response1<bool>
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }

        public async Task<Response1<bool>> FinalizePaidOrder(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, int orderId)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}{orderId}";
                HttpResponseMessage response = await client.PutAsync(url, null);
                string answer = await response.Content.ReadAsStringAsync();

                Response1<bool> ResponseResult = JsonConvert.DeserializeObject<Response1<bool>>(answer);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response1<bool>
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }
                return new Response1<bool>
                {
                    ResultCode = ResultCode.Success,
                    Result = ResponseResult.Result
                };
            }
            catch (Exception ex)
            {
                return new Response1<bool>
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }

        public async Task<Response1<bool>> FinalizeUnPaidOrder(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, int orderId)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{servicePrefix}{controller}{orderId}";
                HttpResponseMessage response = await client.PutAsync(url, null);
                string answer = await response.Content.ReadAsStringAsync();

                Response1<bool> ResponseResult = JsonConvert.DeserializeObject<Response1<bool>>(answer);
                if (ResponseResult.ResultCode != ResultCode.Success)
                {
                    return new Response1<bool>
                    {
                        ResultCode = ResultCode.Warning,
                        ResultMessages = new List<string> { ResponseResult.ResultMessages[0] }
                    };
                }
                return new Response1<bool>
                {
                    ResultCode = ResultCode.Success,
                    Result = ResponseResult.Result
                };
            }
            catch (Exception ex)
            {
                return new Response1<bool>
                {
                    ResultCode = ResultCode.Warning,
                    ResultMessages = new List<string> { ex.Message }
                };
            }
        }
    }
}
