using proyecto_lavera.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proyecto_lavera.InfoServicios;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using proyecto_lavera.Tabbed;

namespace proyecto_lavera.Pagina
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Restaurantes : ContentPage
    {
        private string Slug_Poblacion { get; set; }
        private string Nombre { get; set; }
        private string Url { get; set; }
        public Restaurantes(string slug_poblacion,string nombre,string url)
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
                var restaurantes = await obtenerResul.GetList<ListServicioPoblacion>("http://descubrelavera.com/api/imagenes/poblaciones/" + Slug_Poblacion + "/principal/restaurante");

                if (restaurantes != null)
                {
                    foreach (ListServicioPoblacion bi in restaurantes)
                    {
                        bi.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + bi.Url;
                    }

                    ListRestaurantes.ItemsSource = restaurantes;

                }
                else
                {
                    var restaurantes2 = await obtenerResul.GetObject<ListServicioPoblacion>("http://descubrelavera.com/api/imagenes/poblaciones/" + Slug_Poblacion + "/principal/restaurantes");
                    restaurantes2.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + restaurantes2.Url;
                    List<ListServicioPoblacion> lista2 = new List<ListServicioPoblacion>
                    {
                        restaurantes2
                    };
                    ListRestaurantes.ItemsSource = lista2;

                }

            });
        }

        private void ListRestaurantes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var restaurante = (ListServicioPoblacion)e.SelectedItem;
            Navigation.PushModalAsync(new Restaurante(restaurante.Slug,Nombre));
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