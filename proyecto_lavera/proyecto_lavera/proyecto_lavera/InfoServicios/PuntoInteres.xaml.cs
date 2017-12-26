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
    public partial class PuntoInteres : ContentPage
    {
        public string Slug { get; set; }
        public PuntoInteres(string slug, string nombre)
        {
            InitializeComponent();
            MetodoBoton();
            this.Slug = slug;
            Cabecera.Text = nombre;
        }

        public PuntoInteres(string slug)
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
            Device.BeginInvokeOnMainThread(async () =>
            {
                ObtResult obtenerResul = new ObtResult();
                var punto_interes = await obtenerResul.GetList<ClasePuntoInteres>("http://descubrelavera.com/api/puntos_interes/detalle/" + Slug);

                List<ClaseUrl> list_url = new List<ClaseUrl>();
                if (punto_interes != null)
                {

                    foreach (ClasePuntoInteres pi in punto_interes)
                    {
                        foreach (Imagenes i in pi.Imagenes)
                        {
                            ClaseUrl claseUrl = new ClaseUrl
                            {
                                Url = "http://descubrelavera.com/api/imagenes/mostrar/" + i.Url
                            };
                            list_url.Add(claseUrl);
                        }
                        imagenes.Source = list_url.ElementAt<ClaseUrl>(0).Url;
                        nombre.Text = pi.Nombre.ToUpper();
                        descripcion.Text = pi.Descripcion_Corta;
                        Direccion.Text = pi.Direccion.ToUpper();
                        Punto_interes.Text = pi.Tipo_Punto.ToUpper();
                        Horario.Text = pi.Horario;
                        Cabecera.Text = pi.Poblaciones.ElementAtOrDefault<Poblaciones>(0).Nombre;
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
                    var punto_interes2 = await obtenerResul.GetObject<ClasePuntoInteres>("http://descubrelavera.com/api/puntos_interes/detalle/" + Slug);
                    foreach (Imagenes i in punto_interes2.Imagenes)
                    {
                        ClaseUrl claseUrl = new ClaseUrl
                        {
                            Url = "http://descubrelavera.com/api/imagenes/mostrar/" + i.Url
                        };
                        list_url.Add(claseUrl);
                    }
                    imagenes.Source = list_url.ElementAt<ClaseUrl>(0).Url;
                    nombre.Text = punto_interes2.Nombre.ToUpper();
                    descripcion.Text = punto_interes2.Descripcion_Corta;
                    Direccion.Text = punto_interes2.Direccion.ToUpper();
                    Punto_interes.Text = punto_interes2.Tipo_Punto.ToUpper();
                    Horario.Text = punto_interes2.Horario;
                    Cabecera.Text = punto_interes2.Poblaciones.ElementAtOrDefault<Poblaciones>(0).Nombre;
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