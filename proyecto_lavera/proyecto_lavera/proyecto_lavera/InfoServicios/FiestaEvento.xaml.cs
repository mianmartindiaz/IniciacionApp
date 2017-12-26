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
    public partial class FiestaEvento : ContentPage
    {
        public string Slug { get; set; }
        public FiestaEvento(string slug, string nombre)
        {
            InitializeComponent();
            MetodoBoton();
            this.Slug = slug;
            Cabecera.Text = nombre;
        }

        public FiestaEvento(string slug)
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
                var Fiesta_evento = await obtenerResul.GetList<ClaseFiestaEvento>("http://descubrelavera.com/api/fiestas_eventos/detalle/" + Slug);

                List<ClaseUrl> list_url = new List<ClaseUrl>();
                if (Fiesta_evento != null)
                {

                    foreach (ClaseFiestaEvento fe in Fiesta_evento)
                    {
                        foreach (Imagenes i in fe.Imagenes)
                        {
                            ClaseUrl claseUrl = new ClaseUrl();
                            claseUrl.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + i.Url;
                            list_url.Add(claseUrl);
                        }
                        imagenes.Source = list_url.ElementAt<ClaseUrl>(0).Url;
                        nombre.Text = fe.Nombre.ToUpper();
                        descripcion.Text = fe.Descripcion_Corta;
                        Direccion.Text = fe.Direccion.ToUpper();
                        Fecha.Text = fe.Fecha.ToUpper();
                        Cabecera.Text = fe.Poblaciones.ElementAtOrDefault<Poblaciones>(0).Nombre;
                       
                       

                    }
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
                else
                {
                    var fiesta2 = await obtenerResul.GetObject<ClaseFiestaEvento>("http://descubrelavera.com/api/fiestas_eventos/detalle/" + Slug);
                    foreach (Imagenes i in fiesta2.Imagenes)
                    {
                        ClaseUrl claseUrl = new ClaseUrl();
                        claseUrl.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + i.Url;
                        list_url.Add(claseUrl);
                    }
                    imagenes.Source = list_url.ElementAt<ClaseUrl>(0).Url;
                    nombre.Text = fiesta2.Nombre.ToUpper();
                    descripcion.Text = fiesta2.Descripcion_Corta;
                    Direccion.Text = fiesta2.Direccion.ToUpper();
                    Fecha.Text = fiesta2.Fecha.ToUpper();
                    Cabecera.Text = fiesta2.Poblaciones.ElementAtOrDefault<Poblaciones>(0).Nombre;
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