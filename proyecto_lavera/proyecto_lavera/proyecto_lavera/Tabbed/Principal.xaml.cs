using proyecto_lavera.Pagina;
using proyecto_lavera.Service;
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
    public partial class Principal : TabbedPage
    {
        public Principal()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PushModalAsync(new Salir());
            
            return true;
        }
    }
}