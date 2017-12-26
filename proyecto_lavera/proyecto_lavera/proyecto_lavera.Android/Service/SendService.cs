using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using proyecto_lavera.Service;
using Xamarin.Forms;
using proyecto_lavera.Droid.Service;
using Java.Net;

[assembly: Dependency(typeof(SendService))]
namespace proyecto_lavera.Droid.Service
{
    class SendService : IEmailService
    {
        public void Send(string email)
        {
            String uriText =
    "mailto:"+email;

            var uri = Android.Net.Uri.Parse(uriText);

            Intent sendIntent = new Intent(Intent.ActionSendto);
            sendIntent.SetData(uri);
            Forms.Context.StartActivity(Intent.CreateChooser(sendIntent, "Send email"));
            /*var correo = new Intent(Intent.ActionSend);
            correo.PutExtra(Intent.ExtraEmail, email);
            correo.SetType("plain/text");
            Forms.Context.StartActivity(correo);*/
        }
    }
}