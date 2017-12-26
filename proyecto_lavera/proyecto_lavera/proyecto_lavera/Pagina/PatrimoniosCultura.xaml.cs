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
    public partial class PatrimoniosCultura : ContentPage
    {
        private string Slug_Poblacion { get; set; }
        private string Nombre { get; set; }
        private string Url { get; set; }
        public PatrimoniosCultura(string slug_poblacion,string nombre,string url)
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
                var patrimonios_cultura = await obtenerResul.GetList<ListServicioPoblacion>("http://descubrelavera.com/api/imagenes/poblaciones/" + Slug_Poblacion + "/principal/patrimonio-cultura");

                if (patrimonios_cultura != null)
                {
                    foreach (ListServicioPoblacion bi in patrimonios_cultura)
                    {
                        bi.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + bi.Url;
                    }

                    ListPatrimoniosCultura.ItemsSource = patrimonios_cultura;

                }
                else
                {
                    var patrimonios_cultura2 = await obtenerResul.GetObject<ListServicioPoblacion>("http://descubrelavera.com/api/imagenes/poblaciones/" + Slug_Poblacion + "/principal/patrimonio-cultura");
                    patrimonios_cultura2.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + patrimonios_cultura2.Url;
                    List<ListServicioPoblacion> lista2 = new List<ListServicioPoblacion>
                    {
                        patrimonios_cultura2
                    };
                    ListPatrimoniosCultura.ItemsSource = lista2;

                }

            });
        }

        private void ListPatrimoniosCultura_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var patrimonio = (ListServicioPoblacion)e.SelectedItem;
            Navigation.PushModalAsync(new PatrimonioCultura(patrimonio.Slug,Nombre));
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