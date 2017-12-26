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
using proyecto_lavera.Droid.Service;
using Xamarin.Forms;

[assembly: Dependency(typeof(CloseAplication))]
namespace proyecto_lavera.Droid.Service
{
  
    class CloseAplication : ICloseAplication
    {
        public void CloseApp()
        {
            Process.KillProcess(Process.MyPid());
        }
    }
}