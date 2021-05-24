using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Nfc;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Messaging;
using Xamarin.Forms;

namespace ClubersDeliveryMobile.Prism.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        public MyFirebaseMessagingService()
        {



        }
        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);
            var OrderId = string.Empty;
            if (message.Data.Count > 0)
            {
                OrderId = message.Data["OrderId"];
            }
            MessagingCenter.Send<object, string>(this, "RecibiendoNuevoPedido", OrderId);
        }
        public override void OnNewToken(string p0)
        {
            base.OnNewToken(p0);
        }
    }
}