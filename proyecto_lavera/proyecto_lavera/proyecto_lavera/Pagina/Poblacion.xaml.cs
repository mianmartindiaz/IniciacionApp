using proyecto_lavera.BD;
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
    public partial class Poblacion : ContentPage
    {
        
        private String Slug_Poblacion { get; set; }
        private String Url { get; set; }
        private String Nombre { get; set; }
        public Poblacion()
        {
            InitializeComponent();
        

        }

        public Poblacion(string slug_Poblacion,string url,string nombre)
        {
            InitializeComponent();
            this.Slug_Poblacion = slug_Poblacion;
           // this.Url = url;
            this.Nombre = nombre;
           // img.Source = url;
            titulonombre.Text = nombre;
            
        }
        protected override void OnAppearing()
        {
            CargarServicios();
            CargarCabecera();


        }

        private void ListPoblacion_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var pueblo = (BuscarImagen)e.SelectedItem;
            string opcion = pueblo.Servicio;
            switch (opcion)
            {
                case "Transportes": Navigation.PushModalAsync(new Transportes(Slug_Poblacion,Nombre,pueblo.Url)); break;
                case "Rutas": Navigation.PushModalAsync(new Rutas(Slug_Poblacion, Nombre, pueblo.Url)); break;
                case "Restaurantes": Navigation.PushModalAsync(new Restaurantes(Slug_Poblacion, Nombre, pueblo.Url)); break;
                case "Puntos de interes": Navigation.PushModalAsync(new PuntosInteres(Slug_Poblacion, Nombre, pueblo.Url)); break;
                case "Patrimonio y cultura": Navigation.PushModalAsync(new PatrimoniosCultura(Slug_Poblacion, Nombre, pueblo.Url)); break;
                case "Fiestas y eventos": Navigation.PushModalAsync(new FiestasEventos(Slug_Poblacion, Nombre, pueblo.Url)); break;
                case "Empresas turisticas": Navigation.PushModalAsync(new EmpresasTuristicas(Slug_Poblacion, Nombre, pueblo.Url)); break;
                case "Alojamientos": Navigation.PushModalAsync(new Alojamientos(Slug_Poblacion, Nombre, pueblo.Url)); break;
                case "Actividades y ocio": Navigation.PushModalAsync(new ActividadesOcio(Slug_Poblacion, Nombre, pueblo.Url)); break;
                case "Zonas de baño": Navigation.PushModalAsync(new ZonasDeBanio(Slug_Poblacion, Nombre, pueblo.Url)); break;
                case "Mapa": Navigation.PushModalAsync(new Mapa(Slug_Poblacion, Nombre)); break;
                case "Info": Navigation.PushModalAsync(new Informacion(Slug_Poblacion, pueblo.Url)); break;
            }
        }

        private void CargarCabecera()
        {
            Device.BeginInvokeOnMainThread(async () => {
                ObtResult obtenerResul = new ObtResult();
                var poblacion = await obtenerResul.GetList<BuscarImagen>("http://descubrelavera.com/api/imagenes/poblaciones/cabecera");

                if (poblacion != null)
                {
                    foreach (BuscarImagen bi in poblacion)
                    {
                        bi.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + bi.Url;
                        if (bi.Slug_Poblacion.Equals(Slug_Poblacion))
                        {
                            img.Source = bi.Url;
                        }
                    }

                

                }
               
            });
        }
    
        private void  CargarServicios()
        {
            Device.BeginInvokeOnMainThread(async () => {
                ObtResult obtenerResul = new ObtResult();
                var poblacion = await obtenerResul.GetList<BuscarImagen>("http://descubrelavera.com/api/imagenes/menu-servicio/" + Slug_Poblacion);

                if (poblacion != null)
                {
                    foreach (BuscarImagen bi in poblacion)
                    {
                        bi.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + bi.Url;
                    }

                    ListPoblacion.ItemsSource = poblacion;

                }
                else
                {
                    var poblacion2 = await obtenerResul.GetObject<BuscarImagen>("http://descubrelavera.com/api/menu-servicio/" + Slug_Poblacion);
                    poblacion2.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + poblacion2.Url;
                    List<BuscarImagen> lista2 = new List<BuscarImagen>
                    {
                        poblacion2
                    };
                    ListPoblacion.ItemsSource = lista2;

                }
            });
        }

     
    }
}