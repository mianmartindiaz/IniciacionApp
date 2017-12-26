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
    public partial class FiestasEventos : ContentPage
    {
        private string Slug_Poblacion { get; set; }
        private string Nombre { get; set; }
        private string Url { get; set; }
        public FiestasEventos(string slug_poblacion,string nombre,string url)
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
                var fiestas_eventos = await obtenerResul.GetList<ListServicioPoblacion>("http://descubrelavera.com/api/imagenes/poblaciones/" + Slug_Poblacion + "/principal/fiesta-evento");

                if (fiestas_eventos != null)
                {
                    foreach (ListServicioPoblacion bi in fiestas_eventos)
                    {
                        bi.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + bi.Url;
                    }

                    ListFiestasEventos.ItemsSource = fiestas_eventos;

                }
                else
                {
                    var fiesta_evento = await obtenerResul.GetObject<ListServicioPoblacion>("http://descubrelavera.com/api/imagenes/poblaciones/" + Slug_Poblacion + "/principal/fiesta-evento");
                    fiesta_evento.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + fiesta_evento.Url;
                    List<ListServicioPoblacion> lista2 = new List<ListServicioPoblacion>
                    {
                        fiesta_evento
                    };
                    ListFiestasEventos.ItemsSource = lista2;

                }

            });
        }

        private void ListFiestasEventos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var fiesta = (ListServicioPoblacion)e.SelectedItem;
            Navigation.PushModalAsync(new FiestaEvento(fiesta.Slug,Nombre));
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