using proyecto_lavera.BD;
using proyecto_lavera.Pagina;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using proyecto_lavera.Service;
using System.Diagnostics;

namespace proyecto_lavera.Tabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Poblaciones : ContentPage
    {
        public Poblaciones()
        {
            InitializeComponent();
        }

     
        protected override void OnAppearing()
        {
         
            Device.BeginInvokeOnMainThread(async () => {
                BD.ObtResult obtenerResul = new ObtResult();
                List<BuscarImagen> poblaciones = await obtenerResul.GetList<BuscarImagen>("http://descubrelavera.com/api/imagenes/poblaciones/principal");
                var poblacion = await obtenerResul.GetObject<BuscarImagen>("http://descubrelavera.com/api/imagenes/poblaciones/principal");
                if (poblaciones != null)
                {
                    foreach (BuscarImagen bi in poblaciones)
                    {
                        bi.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + bi.Url;
                    }

                    ListPoblacion.ItemsSource = poblaciones;

                }
                else if (poblacion != null)
                {

                    poblacion.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + poblacion.Url;
                    List<BuscarImagen> lista2 = new List<BuscarImagen>();
                    lista2.Add(poblacion);
                    ListPoblacion.ItemsSource = lista2;
                }
                else await DisplayAlert("", "Error al cargar las poblaciones", "OK");
            });


        }
       
        private void ListPoblacion_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
            var poblacion = (BuscarImagen)e.SelectedItem;
            if(poblacion.Slug_Poblacion.Equals("jarandilla-de-la-vera") || poblacion.Slug_Poblacion.Equals("losar-de-la-vera"))
            {
                Navigation.PushModalAsync(new Poblacion(poblacion.Slug_Poblacion, poblacion.Url, poblacion.Nombre));
            }
            else
            {
                DisplayAlert("Lo sentimos", "App en Demo", "OK");
               
            }
         
           
        }

    }
}