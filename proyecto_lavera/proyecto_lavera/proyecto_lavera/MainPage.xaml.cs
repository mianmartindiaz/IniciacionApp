using proyecto_lavera.BD;
using proyecto_lavera.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace proyecto_lavera
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            if (User.Text != null && Pass.Text != null)
            {
               
                bool a = ComprobarUser(User.Text, Pass.Text);   
                ((NavigationPage)this.Parent).PushAsync(new Tabbed.Principal());
                Navigation.RemovePage(this);
            }
            else DisplayAlert("¡Atención!", "Rellena todo los campos", "OK");
          
        }

        private bool ComprobarUser(String nombre,String pass)
        {
            ISha1 sha1 = DependencyService.Get<ISha1>();
           var passcod= sha1.Codificar(pass);
            Device.BeginInvokeOnMainThread(async () =>
            {
                ObtResult obtenerResul = new ObtResult();
            var usuarios = await obtenerResul.GetList<UsuarioDetail>("http://descubrelavera.com/api/usuarios/detalle/"+nombre+"/"+passcod);

            if (usuarios != null)
            {
               
                foreach(UsuarioDetail cu in usuarios)
                {
                      
                } 
            }
            else
            {
                var usuario = await obtenerResul.GetObject<UsuarioDetail>("http://descubrelavera.com/api/usuarios/detalle/" + nombre + "/" + passcod);
                if (usuario != null)
                {
                        
                    }
                
            }
            });
            return true;
        }

       
    }
}
