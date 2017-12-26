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
    public partial class PuntosInteres : ContentPage
    {
        private string Slug_Poblacion { get; set; }
        private string Nombre { get; set; }
        private string Url { get; set; }
        public PuntosInteres(string slug_poblacion,string nombre,string url)
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
                BD.ObtResult obtenerResul = new ObtResult();
                var puntos_interes = await obtenerResul.GetList<ListServicioPoblacion>("http://descubrelavera.com/api/imagenes/poblaciones/" + Slug_Poblacion + "/principal/punto-interes");

                if (puntos_interes != null)
                {
                    foreach (ListServicioPoblacion bi in puntos_interes)
                    {
                        bi.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + bi.Url;
                    }

                    ListPuntosInteres.ItemsSource = puntos_interes;

                }
                else
                {
                    var puntos_interes2 = await obtenerResul.GetObject<ListServicioPoblacion>("http://descubrelavera.com/api/imagenes/poblaciones/" + Slug_Poblacion + "/principal/punto-interes");
                    puntos_interes2.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + puntos_interes2.Url;
                    List<ListServicioPoblacion> lista2 = new List<ListServicioPoblacion>
                    {
                        puntos_interes2
                    };
                    ListPuntosInteres.ItemsSource = lista2;

                }

            });
        }

        private void ListPuntosInteres_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var puntointeres = (ListServicioPoblacion)e.SelectedItem;
            Navigation.PushModalAsync(new PuntoInteres(puntointeres.Slug,Nombre));
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