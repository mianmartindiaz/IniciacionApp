using proyecto_lavera.BD;
using proyecto_lavera.InfoServicios;
using proyecto_lavera.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace proyecto_lavera.Tabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QR : ContentPage
    {
        public QR()
        {
            InitializeComponent();
            btnScan.Clicked += BtnScan_ClickedAsync;
        }

        private async void BtnScan_ClickedAsync(object sender, EventArgs e)
        {
           var scanner = DependencyService.Get<ICodeScanning>();
            var result = await scanner.ScanAsync();
            if (result != null)
                barcode.Text = result;
        }

    

        private void Button_Clicked(object sender, EventArgs e)
        {
            ListServicios.ItemsSource = null;   
            Device.BeginInvokeOnMainThread(async () =>
            {
                ObtResult obtenerResul = new ObtResult();
                var servicios = await obtenerResul.GetList<ListServicioPoblacion>("http://descubrelavera.com/api/imagenes/buscar/" + buscar.Text);

                if (servicios != null && servicios.Count>0)
                {
                    foreach (ListServicioPoblacion bi in servicios)
                    {
                        bi.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + bi.Url;
                    }

                    ListServicios.ItemsSource = servicios;

                }
                else
                {
                    var servicios2 = await obtenerResul.GetObject<ListServicioPoblacion>("http://descubrelavera.com/api/imagenes/buscar/" + buscar.Text);
                    if (servicios2 != null)
                    {
                        servicios2.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + servicios2.Url;
                        List<ListServicioPoblacion> lista2 = new List<ListServicioPoblacion>
                    {
                        servicios2
                    };
                        ListServicios.ItemsSource = lista2;
                    }
                    else { await DisplayAlert("Vaya", "Busqueda no encontrada", "OK"); }


                }

            });
        }

        private void ListServicios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var servicio = (ListServicioPoblacion)e.SelectedItem;
            switch (servicio.Tipo_Servicio)
            {
                case "zona-bano":Navigation.PushModalAsync(new ZonaBanio(servicio.Slug));  break;
                case "punto-interes": Navigation.PushModalAsync(new PuntoInteres(servicio.Slug)); break;
                case "empresa-turistica": Navigation.PushModalAsync(new EmpresaTuristica(servicio.Slug)); break;
                case "fiesta-evento": Navigation.PushModalAsync(new FiestaEvento(servicio.Slug)); break;
                case "patrimonio-cultura": Navigation.PushModalAsync(new PatrimonioCultura(servicio.Slug)); break;
                case "actividad-ocio": Navigation.PushModalAsync(new ActividadOcio(servicio.Slug)); break;
                case "restaurante": Navigation.PushModalAsync(new Restaurante(servicio.Slug)); break;
                case "alojamiento": Navigation.PushModalAsync(new Alojamiento(servicio.Slug)); break;
                case "transporte": Navigation.PushModalAsync(new Transporte(servicio.Slug)); break;
                case "ruta": Navigation.PushModalAsync(new Ruta(servicio.Slug)); break;
            }
            buscar.Text = "";
            ListServicios.ItemsSource = null;
            
        }
    }
}