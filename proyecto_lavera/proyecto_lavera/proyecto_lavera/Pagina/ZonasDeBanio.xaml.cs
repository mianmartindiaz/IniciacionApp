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
    public partial class ZonasDeBanio : ContentPage
    {
        private string Slug_Poblacion { get; set; }
        private string Nombre { get; set; }
        private string Url { get; set; }
        public ZonasDeBanio(string slug_poblacion,string nombre,string url)
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
                var zonabanio = await obtenerResul.GetList<ListServicioPoblacion>("http://descubrelavera.com/api/imagenes/poblaciones/" + Slug_Poblacion + "/principal/zona-bano");

                if (zonabanio != null)
                {
                    foreach (ListServicioPoblacion bi in zonabanio)
                    {
                        bi.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + bi.Url;
                    }

                    ListZonaBanio.ItemsSource = zonabanio;

                }
                else
                {
                    var zonabanio2 = await obtenerResul.GetObject<BuscarImagen>("http://descubrelavera.com/api/imagenes/poblaciones/" + Slug_Poblacion + "/principal/zona-bano");
                    zonabanio2.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + zonabanio2.Url;
                    List<BuscarImagen> lista2 = new List<BuscarImagen>
                    {
                        zonabanio2
                    };
                    ListZonaBanio.ItemsSource = lista2;

                }

            });
        }

        private void ListZonaBanio_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var zona = (ListServicioPoblacion)e.SelectedItem;
            Navigation.PushModalAsync(new ZonaBanio(zona.Slug,Nombre));
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