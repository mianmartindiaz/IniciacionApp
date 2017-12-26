using proyecto_lavera.BD;
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
	public partial class Informacion : ContentPage
	{
        private string Slug_Poblacion { get; set; }
        private string Url { get; set; }
        public Informacion (string slug_poblacion,string url)
		{
			InitializeComponent ();
            MetodoBoton();
            this.Slug_Poblacion = slug_poblacion;
            this.Url = url;
		}

        protected override void OnAppearing()
        {
            CargarInfo();
            CargarComentarios();
          
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

        private void CargarComentarios()
        {
            Device.BeginInvokeOnMainThread(async () => {
                ObtResult obtenerResul = new ObtResult();
                var comentario = await obtenerResul.GetList<ComentarioListPoblacion>("http://descubrelavera.com/api/comentarios/poblaciones/" + Slug_Poblacion);
                var comentario2 = await obtenerResul.GetObject<ComentarioListPoblacion>("http://descubrelavera.com/api/comentarios/poblaciones" + Slug_Poblacion);
                if (comentario != null)
                {
                    foreach(ComentarioListPoblacion clp in comentario)
                    {
                        clp.Puntuacion += "/5 puntos";
                    }
                    ListOpiniones.ItemsSource = comentario;

                }
                else if(comentario2!=null)
                {
                    List<ComentarioListPoblacion> lista2 = new List<ComentarioListPoblacion>();
                    comentario2.Puntuacion += "/5 puntos ";
                    lista2.Add(comentario2);
                    ListOpiniones.ItemsSource = lista2;

                }
            });
        }
        private void CargarInfo()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                BD.ObtResult obtenerResul = new ObtResult();
                var info = await obtenerResul.GetList<PoblacionDetail>("http://descubrelavera.com/api/poblaciones/" + Slug_Poblacion);

                if (info != null)
                {
                    Cabecera.Text = info.ElementAt<PoblacionDetail>(0).Nombre;
                    imagenes.Source = Url;
                    descripcion.Text = info.ElementAt<PoblacionDetail>(0).Descripcion_Corta;
                    Habitantes.Text = Convert.ToString(info.ElementAt<PoblacionDetail>(0).Habitantes);
                    Extension.Text = info.ElementAt<PoblacionDetail>(0).Extension;

                }
                else
                {
                    var info2 = await obtenerResul.GetObject<PoblacionDetail>("http://descubrelavera.com/api/poblaciones/" + Slug_Poblacion);
                    Cabecera.Text = info2.Nombre.ToUpper();
                    imagenes.Source = Url;
                    descripcion.Text = info2.Descripcion_Corta;
                    Habitantes.Text = Convert.ToString(info2.Habitantes).ToUpper();
                    Extension.Text = info2.Extension + " m2".ToUpper();
                }

            });
        }

        private void ListOpiniones_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}