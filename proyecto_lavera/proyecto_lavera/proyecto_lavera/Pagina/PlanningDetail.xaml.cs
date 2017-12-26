using proyecto_lavera.BD;
using proyecto_lavera.InfoServicios;
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
    public partial class PlanningDetail : ContentPage
    {
        private int Id { get; set; }
        public PlanningDetail(int id)
        {
            InitializeComponent();
            this.Id = id;
        }

        protected override void OnAppearing()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
               
                ObtResult obtenerResul = new ObtResult();
                var detalles = await obtenerResul.GetList<ClasePlanningDetail>("http://descubrelavera.com/api/plannings_lugares/detalle/"+Id);

                if (detalles != null && detalles.Count>0)
                {
                    
                    ListPlanningDetail.ItemsSource = detalles;

                }
                else
                {
                    var detalle = await obtenerResul.GetObject<ClasePlanningDetail>("http://descubrelavera.com/api/plannings_lugares/detalle/" + Id);
                
                    List<ClasePlanningDetail> lista2 = new List<ClasePlanningDetail>
                    {
                        detalle
                    };
                    ListPlanningDetail.ItemsSource = lista2;

                }

            });
        }
        private void ListPlanningDetail_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var servicio = (ClasePlanningDetail)e.SelectedItem;
            switch (servicio.Tipo_Servicio)
            {
                case "zona-bano": Navigation.PushModalAsync(new ZonaBanio(servicio.Slug)); break;
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
        }
    }
}