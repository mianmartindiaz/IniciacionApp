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
using System.Security.Cryptography;
using Xamarin.Forms;
using proyecto_lavera.Droid.Service;

[assembly: Dependency(typeof(Codificar))]

namespace proyecto_lavera.Droid.Service
{
  
    class Codificar : ISha1
    {
        string ISha1.Codificar(string pass)
        {
             var e = new SHA1Managed();
            byte[] data = e.ComputeHash(Encoding.UTF8.GetBytes(pass));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++) {
                sBuilder.Append(data[i].ToString("x2")); } 
            string input = sBuilder.ToString(); return (input); } 
        }
    }
