using Xamarin.Forms;

namespace ClubersDeliveryMobile.Prism
{
    public static class Constants
    {
        public static string HostName { get; set; } = "https://chatserverclubers.azurewebsites.net";
        public static string MessageName { get; set; } = "newMessage";
        public static string Username
        {
            get
            {
                return $"{Device.RuntimePlatform} User";
            }
        }
        public const string ErrorMessage = "Error en el app";
        public const string AcceptMessage = "Aceptar";
        public const string ConnectionError = "Error de Conexion a Internet";
        public const string GoogleMapsApiKey = "AIzaSyCQuwuhH4ACaaLBOpC-FEQvMSuKEnh86AE";
        public const string urlBase = "http://clubers.qagperti.tk/";
        public const string servicePrefix = "api";
        public const string tokenType = "bearer";
        public const string accessToken = "";

        //Controllers

        
        public const string ResetPasswordController = "/Account/resetpassword";
        public const string GetUserByEmailAsyncController = "/Account/GetUserByEmailAsync";
        public const string GetTokenController = "/Account/token";
        public const string GetProfileSRByIdAsyncController = "/HumanResources/GetProfileSRByIdAsync/";
        public const string GetDeliveriesSRAsyncController = "/Order/GetDeliveriesSRAsync/";
        public const string UpdateStatusDeliveryManAsyncController = "/HumanResources/UpdateStatusDeliveryManAsync/";
        public const string GetEarningsSRAsyncController = "/HumanResources/GetEarningsAppSRAsync/";
        public const string GetPaymentsCashAppSRAsyncController = "/HumanResources/PaymentsCashAppSRAsync/";
        public const string GetAccountStatusAppSRAsyncController = "/HumanResources/GetAccountStatusAppSRAsync/";
        public const string GetStatusDeliveryMenAppSRAsyncController = "/HumanResources/GetStatusDeliveryMenAppSRAsync/";
        public const string SetCurrentLocationAppSRAsyncController = "/HumanResources/SetCurrentLocationAppSRAsync";
        public const string AcceptOrderAppSRAsyncController = "/Order/AcceptOrderAppSRAsync/";
        public const string RejectOrderAppSRAsyncController = "/Order/RejectOrderAppSRAsync/";
        public const string GetOrderDetailAppSRAsyncController = "/Order/GetOrderDetailAppSRAsync/";
        public const string FinalizePaidOrderAppSRAsyncController = "/Order/FinalizePaidOrderAppSRAsync/";
        public const string FinalizeUnpaidOrderAppSRAsyncController = "/Order/FinalizeUnpaidOrderAppSRAsync/";

        //Whatsapp
        public const string wa1 = "https://wa.me/+5215586765674?text=Tengo%20un%20problema%20con%20un%20pedido";
        public const string wa2 = "https://wa.me/+5215586765674?text=Tengo%20un%20problema%20con%20un%20Socio%20Consumidor";
        public const string wa3 = "https://wa.me/+5215586765674?text=Tengo%20un%20problema%20con%20un%20cobro";
        public const string wa4 = "https://wa.me/+5215586765674?text=Tengo%20un%20problema%20con%20un%20Saldo";
    }
}
