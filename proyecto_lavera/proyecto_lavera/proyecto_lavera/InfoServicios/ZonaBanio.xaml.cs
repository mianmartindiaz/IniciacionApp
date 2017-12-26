using proyecto_lavera.BD;
using proyecto_lavera.Pagina;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace proyecto_lavera.InfoServicios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZonaBanio : ContentPage
    {
        public string Slug { get; set; }
        public ZonaBanio(string slug, string nombre)
        {
            InitializeComponent();
            MetodoBoton();
            Cabecera.Text = nombre;
            this.Slug = slug;
        }

        public ZonaBanio(string slug)
        {
            InitializeComponent();
            this.Slug = slug;
        }

        protected override void OnAppearing()
        {
            CargarDetalles();
          
        }

        private void CargarDetalles()
        {
            Label accesibleCoche = new Label { FontSize = 1,FontAttributes=FontAttributes.Bold,Text="NO"};
            Label parking = new Label { FontSize = 1, FontAttributes = FontAttributes.Bold ,Text = "NO" };
            Label zonajuego = new Label { FontSize = 1, FontAttributes = FontAttributes.Bold ,Text = "NO" };
            Label zonamerendero = new Label { FontSize = 1, FontAttributes = FontAttributes.Bold ,Text = "NO" };
            Label entradaanimales = new Label { FontSize = 1, FontAttributes = FontAttributes.Bold, Text = "NO" };
            Label restaurantes = new Label { FontSize = 1, FontAttributes = FontAttributes.Bold, Text = "NO" };
            Label zonaacampada = new Label { FontSize = 1, FontAttributes = FontAttributes.Bold, Text = "NO" };
            Device.BeginInvokeOnMainThread(async () =>
            {
                ObtResult obtenerResul = new ObtResult();
                var zonabano = await obtenerResul.GetList<ClaseZonaBano>("http://descubrelavera.com/api/zonas_bano/detalle/" + Slug);

                List<ClaseUrl> list_url = new List<ClaseUrl>();
                if (zonabano != null)
                {

                    foreach (ClaseZonaBano zb in zonabano)
                    {
                        foreach (Imagenes i in zb.Imagenes)
                        {
                            ClaseUrl claseUrl = new ClaseUrl
                            {
                                Url = "http://descubrelavera.com/api/imagenes/mostrar/" + i.Url
                            };
                            list_url.Add(claseUrl);
                        }
                        imagenes.Source = list_url.ElementAt<ClaseUrl>(0).Url;
                        nombre.Text = zb.Nombre.ToUpper();
                        descripcion.Text = zb.Descripcion_Corta;
                        Direccion.Text = zb.Direccion.ToUpper();

                        if (zb.Accesible_Coche) accesibleCoche.Text = "SI";
                        AccesibleCoche.Text += accesibleCoche.Text;

                        if (zb.Parking) parking.Text = "SI";
                        Parking.Text += parking.Text;

                        if (zb.Zona_Juegos) zonajuego.Text = "SI";
                        ZonaJuegos.Text += zonajuego.Text;

                        if (zb.Zona_Merendero) zonamerendero.Text = "SI";
                        ZonaMerenderos.Text += zonamerendero.Text;

                        if (zb.Zona_Acampada) zonaacampada.Text = "SI";
                        ZonaAcampada.Text += zonaacampada.Text;

                        if (zb.Entrada_Animales) entradaanimales.Text = "SI";
                        EntradaAnimales.Text += entradaanimales.Text;

                        if (zb.Restaurantes) restaurantes.Text = "SI";
                        Restaurantes.Text += restaurantes.Text;

                        Cabecera.Text = zb.Poblaciones.ElementAtOrDefault<Poblaciones>(0).Nombre;
                        int cont = 1;
                        if (list_url.Count > 1)
                        {
                            Device.StartTimer(TimeSpan.FromSeconds(3), () =>
                            {
                                if (list_url.Count <= cont) cont = 0;
                                imagenes.Source = list_url.ElementAt<ClaseUrl>(cont).Url;
                                cont++;
                                return true;
                            }
                                                      );
                        }
                    }
                }
                else
                {
                    var zona2 = await obtenerResul.GetObject<ClaseZonaBano>("http://descubrelavera.com/api/zonas_bano/detalle/" + Slug);
                    foreach (Imagenes i in zona2.Imagenes)
                    {
                        ClaseUrl claseUrl = new ClaseUrl
                        {
                            Url = "http://descubrelavera.com/api/imagenes/mostrar/" + i.Url
                        };
                        list_url.Add(claseUrl);
                    }
                    imagenes.Source = list_url.ElementAt<ClaseUrl>(0).Url;
                    nombre.Text = zona2.Nombre.ToUpper();
                    descripcion.Text = zona2.Descripcion_Corta;
                    Direccion.Text = zona2.Direccion.ToUpper();

                    if (zona2.Accesible_Coche) accesibleCoche.Text = "SI";
                    AccesibleCoche.Text += accesibleCoche.Text;

                    if (zona2.Parking) parking.Text = "SI";
                    Parking.Text += parking.Text;

                    if (zona2.Zona_Juegos) zonajuego.Text = "SI";
                    ZonaJuegos.Text += zonajuego.Text;

                    if (zona2.Zona_Merendero) zonamerendero.Text = "SI";
                    ZonaMerenderos.Text += zonamerendero.Text;

                    if (zona2.Zona_Acampada) zonaacampada.Text = "SI";
                    ZonaAcampada.Text += zonaacampada.Text;

                    if (zona2.Entrada_Animales) entradaanimales.Text = "SI";
                    EntradaAnimales.Text += entradaanimales.Text;

                    if (zona2.Restaurantes) restaurantes.Text = "SI";
                    Restaurantes.Text += restaurantes.Text;
                    Cabecera.Text = zona2.Poblaciones.ElementAtOrDefault<Poblaciones>(0).Nombre;
                    int cont = 1;
                    if (list_url.Count > 1)
                    {
                        Device.StartTimer(TimeSpan.FromSeconds(3), () =>
                        {
                            if (list_url.Count <= cont) cont = 0;
                            imagenes.Source = list_url.ElementAt<ClaseUrl>(cont).Url;
                            cont++;
                            return true;
                        }
                                                  );
                    }
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

        private void BtnComentarios_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Comentarios(Slug, Cabecera.Text));
        }
    }
}