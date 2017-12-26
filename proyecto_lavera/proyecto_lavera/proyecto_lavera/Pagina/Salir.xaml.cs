using proyecto_lavera.Service;
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
    public partial class Salir : ContentPage
    {
        public Salir()
        {
            InitializeComponent();
        }

        private void Exit_Clicked(object sender, EventArgs e)
        {
            ICloseAplication closeAplication = DependencyService.Get<ICloseAplication>();
            closeAplication.CloseApp();
        }

        private void NoSalir_Clicked(object sender, EventArgs e)
        {
            base.OnBackButtonPressed();
        }
       
    }
}