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
    public partial class Animacion : ContentPage
    {
        public Animacion()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.Content.IsEnabled = false;
            Imagen.FadeTo(1, 4000);
            Device.StartTimer(TimeSpan.FromSeconds(3), TimerCallBack);
        }
        private bool TimerCallBack()
        {
            ((NavigationPage)this.Parent).PushAsync(new Principal());
            Navigation.RemovePage(this);
            return false;
        }
    }
}