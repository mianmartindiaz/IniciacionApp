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
	public partial class Deseos : ContentPage
	{
        private int Id { get; set; }
		public Deseos (int id)
		{
			InitializeComponent ();
            MetodoBoton();
            this.Id = id;
		}

        protected override void OnAppearing()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                ClaseDeseos apoyo = null;
                List<ClaseDeseos> listapoyo = new List<ClaseDeseos>();
                ObtResult obtenerResul = new ObtResult();
                var deseos = await obtenerResul.GetList<ListaDeseosList>("http://descubrelavera.com/api/lista_deseos/lista/"+Id);

                if (deseos != null)
                {
                    foreach (ListaDeseosList bi in deseos)
                    {
                        apoyo = new ClaseDeseos();
                        apoyo.Poblacion = bi.Poblacion.ElementAt<Poblaciones>(0).Nombre;
                        apoyo.Slug = bi.Slug;
                        apoyo.Lugar_Actividad = bi.Lugar_Actividad;
                        apoyo.Tipo_Lugar = bi.Tipo_Lugar;
                        foreach (Imagenes i in bi.Imagenes)
                        {
                            if (i.Tipo_foto.Equals("principal"))
                            {
                                i.Url= "http://descubrelavera.com/api/imagenes/mostrar/" + i.Url;
                                apoyo.Url = i.Url;
                            }
                        }
                        listapoyo.Add(apoyo);
                       
                    }

                    ListActvidad_Deseos.ItemsSource = listapoyo;

                }
                else
                {
                    var deseo = await obtenerResul.GetObject<ListaDeseosList>("http://descubrelavera.com/api/lista_deseos/lista/" + Id);
                    apoyo = new ClaseDeseos();
                    apoyo.Poblacion = deseo.Poblacion.ElementAt<Poblaciones>(0).Nombre;
                    apoyo.Slug = deseo.Slug;
                    apoyo.Lugar_Actividad = deseo.Lugar_Actividad;
                    apoyo.Tipo_Lugar = deseo.Tipo_Lugar;
                    foreach (Imagenes i in deseo.Imagenes)
                    {
                        if (i.Tipo_foto.Equals("principal"))
                        {
                            i.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + i.Url;
                            apoyo.Url = i.Url;
                        }
                    }
                    listapoyo.Add(apoyo);
                    ListActvidad_Deseos.ItemsSource = listapoyo;

                }

            });
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
            Navigation.PushModalAsync(new Tabbed.Principal());
        }

        private void ListActvidad_Deseos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var deseo = (ClaseDeseos)e.SelectedItem;
            switch (deseo.Tipo_Lugar)
            {
                case "zona-bano": Navigation.PushModalAsync(new ZonaBanio(deseo.Slug)); break;
                case "punto-interes": Navigation.PushModalAsync(new PuntoInteres(deseo.Slug)); break;
                case "empresa-turistica": Navigation.PushModalAsync(new EmpresaTuristica(deseo.Slug)); break;
                case "fiesta-evento": Navigation.PushModalAsync(new FiestaEvento(deseo.Slug)); break;
                case "patrimonio-cultura": Navigation.PushModalAsync(new PatrimonioCultura(deseo.Slug)); break;
                case "actividad-ocio": Navigation.PushModalAsync(new ActividadOcio(deseo.Slug)); break;
                case "restaurante": Navigation.PushModalAsync(new Restaurante(deseo.Slug)); break;
                case "alojamiento": Navigation.PushModalAsync(new Alojamiento(deseo.Slug)); break;
                case "transporte": Navigation.PushModalAsync(new Transporte(deseo.Slug)); break;
                case "ruta": Navigation.PushModalAsync(new Ruta(deseo.Slug)); break;
            }
        }
    }
}