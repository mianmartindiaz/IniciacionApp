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
    public partial class Rutas : ContentPage
    {
        private string Slug_Poblacion { get; set; }
        private string Nombre { get; set; }
        private string Url { get; set; }
        public Rutas(string slug_Poblacion,string nombre,string url)
        {
            InitializeComponent();
            MetodoBoton();
            this.Slug_Poblacion = slug_Poblacion;
            this.Nombre = nombre;
            this.Url = url;
        }
        protected override void OnAppearing()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                Cabecera.Text = Nombre;
                BD.ObtResult obtenerResul = new ObtResult();
                var rutas = await obtenerResul.GetList<ListServicioPoblacion>("http://descubrelavera.com/api/imagenes/poblaciones/" + Slug_Poblacion + "/principal/ruta");

                if (rutas != null)
                {
                    foreach (ListServicioPoblacion bi in rutas)
                    {
                        bi.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + bi.Url;
                    }

                     ListRutas.ItemsSource = rutas;

                }
                else
                {
                    var rutas2 = await obtenerResul.GetObject<ListServicioPoblacion>("http://descubrelavera.com/api/imagenes/poblaciones/" + Slug_Poblacion + "/principal/ruta");
                    rutas2.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + rutas2.Url;
                    List<ListServicioPoblacion> lista2 = new List<ListServicioPoblacion>
                    {
                        rutas2
                    };
                    ListRutas.ItemsSource = lista2;

                }

            });
        }

        private void ListRutas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var ruta = (ListServicioPoblacion)e.SelectedItem;
            Navigation.PushModalAsync(new Ruta(ruta.Slug,Nombre));
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