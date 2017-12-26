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
    public partial class Comentarios : ContentPage
    {
        public String Slug { get; set; }
        
        public Comentarios(string slug,string nombre)
        {
            InitializeComponent();
            this.Slug = slug;
            Cabecera.Text = nombre;
            MetodoBoton();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(async () => {
                ObtResult obtenerResul = new ObtResult();
                var comentario = await obtenerResul.GetList<ComentarioListServicio>("http://descubrelavera.com/api/comentarios/servicios/" + Slug);
                var comentario2 = await obtenerResul.GetObject<ComentarioListServicio>("http://descubrelavera.com/api/comentarios/servicios" + Slug);
                if (comentario != null)
                {
                    foreach (ComentarioListServicio clp in comentario)
                    {
                        clp.Puntuacion += "/5 puntos";
                    }
                    ListOpiniones.ItemsSource = comentario;

                }
                else if (comentario2 != null)
                {
                    List<ComentarioListServicio> lista2 = new List<ComentarioListServicio>();
                    comentario2.Puntuacion += "/5 puntos ";
                    lista2.Add(comentario2);
                    ListOpiniones.ItemsSource = lista2;

                }
            });
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
            Navigation.PushModalAsync(new Tabbed.Principal());
        }

        private void ListOpiniones_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}