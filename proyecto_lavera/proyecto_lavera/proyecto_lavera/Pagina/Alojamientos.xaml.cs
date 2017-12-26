using proyecto_lavera.BD;
using proyecto_lavera.InfoServicios;
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
    public partial class Alojamientos : ContentPage
    {
        private string Slug_Poblacion { get; set; }
        private string Nombre { get; set; }
        private string Url { get; set; }
        public Alojamientos(string slug_poblacion,string nombre,string url)
        {
            InitializeComponent();
            MetodoBoton();
            this.Slug_Poblacion = slug_poblacion;
            this.Nombre = nombre;
            this.Url = url;
        }

        protected override void OnAppearing()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                Cabecera.Text = Nombre;
                ObtResult obtenerResul = new ObtResult();
                var alojamientos = await obtenerResul.GetList<ListServicioPoblacion>("http://descubrelavera.com/api/imagenes/poblaciones/" + Slug_Poblacion + "/principal/alojamiento");

                if (alojamientos != null)
                {
                    foreach (ListServicioPoblacion bi in alojamientos)
                    {
                        bi.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + bi.Url;
                    }

                    ListAlojamientos.ItemsSource = alojamientos;

                }
                else
                {
                    var alojamientos2 = await obtenerResul.GetObject<ListServicioPoblacion>("http://descubrelavera.com/api/imagenes/poblaciones/" + Slug_Poblacion + "/principal/alojamiento");
                    alojamientos2.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + alojamientos2.Url;
                    List<ListServicioPoblacion> lista2 = new List<ListServicioPoblacion>
                    {
                        alojamientos2
                    };
                    ListAlojamientos.ItemsSource = lista2;

                }

            });
        }

        private void ListAlojamientos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var actvidad_ocio = (ListServicioPoblacion)e.SelectedItem;
            Navigation.PushModalAsync(new Alojamiento(actvidad_ocio.Slug,Nombre));
        }
        protected void MetodoBoton()
        {
            var tapimage = new TapGestureRecognizer();
            tapimage.Tapped += TapimageCarta;
            menu.GestureRecognizers.Add(tapimage);

        }

        void TapimageCarta(Object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
            Navigation.PushModalAsync(new Principal());
        }

    }
}