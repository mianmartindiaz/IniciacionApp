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
    public partial class Planning : ContentPage
    {
        public Planning()
        {
            InitializeComponent();
            MetodoBoton();
        }

        protected override void OnAppearing()
        {
            DataAccess dataAcess = new DataAccess();
            Device.BeginInvokeOnMainThread(async () =>
            {
              
                ObtResult obtenerResul = new ObtResult();
                var plannings = await obtenerResul.GetList<PlanningList>("http://descubrelavera.com/api/plannings/lista/"+dataAcess.GetUsuario().Id_User);

                if (plannings != null)
                {
                    ListPlanning.ItemsSource = plannings;

                }
                else
                {
                    var actividad_ocio2 = await obtenerResul.GetObject<PlanningList>("http://descubrelavera.com/api/plannings/lista/" + dataAcess.GetUsuario().Id_User);
                  
                    List<PlanningList> lista2 = new List<PlanningList>
                    {
                        actividad_ocio2
                    };
                    ListPlanning.ItemsSource = lista2;

                }

            });
        }
        private void ListPlanning_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var planning = (PlanningList)e.SelectedItem;
            Navigation.PushModalAsync(new PlanningDetail(planning.Id));
        }

        protected void MetodoBoton()
        {
            var tapimage = new TapGestureRecognizer();
            tapimage.Tapped += TapimageMenu;
            menu.GestureRecognizers.Add(tapimage);

        }

        void TapimageMenu(Object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
            Navigation.PushModalAsync(new Principal());
        }
    }
}