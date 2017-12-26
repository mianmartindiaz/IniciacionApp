using proyecto_lavera.Pagina;
using proyecto_lavera.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace proyecto_lavera.Tabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MiCuenta : ContentPage
    {
        private DataAccess dataAccess = new DataAccess();
        public MiCuenta()
        {
            InitializeComponent();
            MetodoBoton();
        }

        private void BtnRegistro_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Registro());
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var numUser = ExistUser();
            if (numUser==0)
            {
                BtnRegistro.IsVisible = true;
                nombre.Text = "Bienvenido ";
            }
            else
            {
                BtnRegistro.IsVisible = false;
                nombre.Text ="Bienvenido "+ dataAccess.GetUsuario().Name;
            }
        }
        private int ExistUser()
        {
            var cont= dataAccess.getCountUsuario();
            return cont;
        }

        protected void MetodoBoton()
        {
            var tapimagedeseos = new TapGestureRecognizer();
            tapimagedeseos.Tapped += TapimageDeseos;
            deseos.GestureRecognizers.Add(tapimagedeseos);
            var tapimageplanning = new TapGestureRecognizer();
            tapimageplanning.Tapped += TapimagePlanning;
            planning.GestureRecognizers.Add(tapimageplanning);

        }

        void TapimageDeseos(Object sender, EventArgs e)
        {
            var num = dataAccess.getCountUsuario();
            if (num > 0)
            {
                Navigation.PopModalAsync();
                var id = dataAccess.GetUsuario().Id_User;
                Navigation.PushModalAsync(new Deseos(id));
            }
            else DisplayAlert("No estás logueado o registrado", "Si no estás registrado: http://descubrelavera.com", "OK");
           
        }

        void TapimagePlanning(Object sender, EventArgs e)
        {
            var num = dataAccess.getCountUsuario();
            if (num > 0)
            {
            Navigation.PopModalAsync();
            Navigation.PushModalAsync(new Planning());
            }
            else DisplayAlert("No estás logueado o registrado", "Si no estás registrado: http://descubrelavera.com", "OK");
        }
    }
}