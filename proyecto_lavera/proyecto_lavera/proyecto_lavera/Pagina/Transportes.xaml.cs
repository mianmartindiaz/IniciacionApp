using proyecto_lavera.BD;
using proyecto_lavera.InfoServicios;
using proyecto_lavera.Tabbed;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace proyecto_lavera.Pagina
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Transportes : ContentPage
    {
        private String Slug_Poblacion { get; set; }
        private String Nombre { get; set; }
        private string Url { get; set; }
        public Transportes(string slug_Poblacion, string nombre, string url)
        {
            NavigationPage.SetHasNavigationBar(this, true);
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
                BD.ObtResult obtenerResul = new ObtResult();
                var transportes = await obtenerResul.GetList<ListServicioPoblacion>("http://descubrelavera.com/api/imagenes/poblaciones/" + Slug_Poblacion + "/principal/transporte");

                if (transportes != null)
                {
                    foreach (ListServicioPoblacion bi in transportes)
                    {
                        bi.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + bi.Url;
                    }
                    Cabecera.Text = Nombre;
                    ListTransporte.ItemsSource = transportes;

                }
                else
                {
                    var transportes2 = await obtenerResul.GetObject<ListServicioPoblacion>("http://descubrelavera.com/api/imagenes/poblaciones/" + Slug_Poblacion + "/principal/transporte");
                    transportes2.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + transportes2.Url;
                    List<ListServicioPoblacion> lista2 = new List<ListServicioPoblacion>
                    {
                        transportes2
                    };
                    Cabecera.Text = Nombre;
                    ListTransporte.ItemsSource = lista2;
                 
                }

            });
        }

        private void ListTransporte_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var transporte = (ListServicioPoblacion)e.SelectedItem;
            Navigation.PushModalAsync(new Transporte(transporte.Slug,Nombre));
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