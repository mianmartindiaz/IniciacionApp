using System;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Android.Content;
using Xamarin.Forms;
using proyecto_lavera.Droid.Service;
using proyecto_lavera.Service;

[assembly: Dependency(typeof(CallService))]
namespace proyecto_lavera.Droid.Service
{
    public class CallService : ICallService
    {
        public static void Init()
        {

        }

        public void MakeCall(string phone)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(phone, "^(\\(?\\+?[0-9]*\\)?)?[0-9_\\- \\(\\)]*$"))
            {
                var uri = Android.Net.Uri.Parse(String.Format("tel:{0}", phone));
                var intent = new Intent(Intent.ActionView, uri);
                Xamarin.Forms.Forms.Context.StartActivity(intent);
            }
            else
            {
                
                 
              
            }
        }
    }
}