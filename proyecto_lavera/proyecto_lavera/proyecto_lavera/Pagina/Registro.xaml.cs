using proyecto_lavera.BD;
using proyecto_lavera.Sqlite;
using proyecto_lavera.Tabbed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace proyecto_lavera.Pagina
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Registro : ContentPage
	{
		public Registro ()
		{
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            if (User.Text != null && Pass.Text != null)
            {

                bool a = ComprobarUser(User.Text, Pass.Text);
               
            }
            else DisplayAlert("¡Atención!", "Rellena todo los campos", "OK");

        }

        private bool ComprobarUser(String nombre, String pass)
        {
          
            Device.BeginInvokeOnMainThread(async () =>
            {
                ObtResult obtenerResul = new ObtResult();
                var usuarios = await obtenerResul.GetList<UsuarioDetail>("http://descubrelavera.com/api/usuarios/detalle/" + nombre + "/" + pass);

                if (usuarios != null)
                {

                    foreach (UsuarioDetail cu in usuarios)
                    {
                        Usuarios user = new Usuarios(cu.Nombre,cu.Email,cu.Id);
                        DataAccess dataAccess = new DataAccess();
                        dataAccess.InsertUsuario(user);
                        await Navigation.PopModalAsync();
                      
                    }
                }
                else
                {
                    var usuario = await obtenerResul.GetObject<UsuarioDetail>("http://descubrelavera.com/api/usuarios/detalle/" + nombre + "/" + pass);
                    if (usuario != null)
                    {
                        Usuarios user = new Usuarios(usuario.Nombre, usuario.Email,usuario.Id);
                        DataAccess dataAccess = new DataAccess();
                        dataAccess.InsertUsuario(user);
                          await Navigation.PopModalAsync();
                      
                    }

                }
            });
            return true;
        }


    }
}