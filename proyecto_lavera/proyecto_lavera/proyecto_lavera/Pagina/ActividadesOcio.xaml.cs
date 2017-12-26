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
    public partial class ActividadesOcio : ContentPage
    {
        private string Slug_Poblacion { get; set; }
        private string Nombre { get; set; }
        private string Url { get; set; }
        public ActividadesOcio(string slug_poblacion,string nombre,string url)
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
                var actividad_ocio = await obtenerResul.GetList<ListServicioPoblacion>("http://descubrelavera.com/api/imagenes/poblaciones/" + Slug_Poblacion + "/principal/actividad-ocio");

                if (actividad_ocio != null)
                {
                    foreach (ListServicioPoblacion bi in actividad_ocio)
                    {
                        bi.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + bi.Url;
                    }

                    ListActvidad_Ocio.ItemsSource = actividad_ocio;

                }
                else
                {
                    var actividad_ocio2 = await obtenerResul.GetObject<ListServicioPoblacion>("http://descubrelavera.com/api/imagenes/poblaciones/" + Slug_Poblacion + "/principal/actividad-ocio");
                    actividad_ocio2.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + actividad_ocio2.Url;
                    List<ListServicioPoblacion> lista2 = new List<ListServicioPoblacion>
                    {
                        actividad_ocio2
                    };
                    ListActvidad_Ocio.ItemsSource = lista2;

                }

            });
        }

        private void ListActvidad_Ocio_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var actvidad_ocio = (ListServicioPoblacion)e.SelectedItem;
            Navigation.PushModalAsync(new ActividadOcio(actvidad_ocio.Slug,Nombre));
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