using proyecto_lavera.BD;
using proyecto_lavera.Pagina;
using proyecto_lavera.Service;
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
    public partial class Restaurante : ContentPage
    {
        private string Slug { get; set; }
        private String Telefono { get; set; }
        private String Correo { get; set; }
        private String Web { get; set; }
        public Restaurante(string slug, string nombre)
        {
            InitializeComponent();
            MetodoBoton();
            this.Slug = slug;
            Cabecera.Text = nombre;
        }

        public Restaurante(string slug)
        {
            InitializeComponent();
            this.Slug = slug;
            MetodoBoton();
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
            var restaurante = await obtenerResul.GetList<RestauranteDetail>("http://descubrelavera.com/api/restaurantes/detalle/" + Slug);
                ClaseUrl claseUrl = null;
            List<ClaseUrl> list_url = new List<ClaseUrl>();
            if (restaurante != null)
            {

                foreach (RestauranteDetail r in restaurante)
                {
                    foreach (Imagenes i in r.Imagenes)
                    {
                            claseUrl = new ClaseUrl();
                        claseUrl.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + i.Url;
                        list_url.Add(claseUrl);
                    }

                    imagenes.Source = list_url.ElementAt<ClaseUrl>(0).Url;
                    Puntuacion.Text = r.Puntuacion_Total + " sobre 5";
                    nombre.Text = r.Nombre.ToUpper();
                    descripcion.Text = r.Descripcion_Corta;
                    Direccion.Text = r.Direccion.ToUpper();
                    Web = r.Web;
                    Tipo_comida.Text = r.Tipo_comida.ToUpper();
                    Categoria.Text = r.Categoria.ToUpper();
                    Telefono = r.Telefono_uno;
                    Correo = r.Email;
                    Cabecera.Text = r.Poblaciones.ElementAt<Poblaciones>(0).Nombre;
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
                    var restaurante2 = await obtenerResul.GetObject<RestauranteDetail>("http://descubrelavera.com/api/restaurantes/detalle/" + Slug);
                    foreach (Imagenes i in restaurante2.Imagenes)
                    {
                        claseUrl = new ClaseUrl();
                        claseUrl.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + i.Url;
                        list_url.Add(claseUrl);
                    }
                    imagenes.Source = list_url.ElementAt<ClaseUrl>(0).Url;
                    Puntuacion.Text = restaurante2.Puntuacion_Total+" sobre 5";
                    nombre.Text = restaurante2.Nombre.ToUpper();
                    descripcion.Text = restaurante2.Descripcion_Corta;
                    Direccion.Text = restaurante2.Direccion.ToUpper();
                    Web = restaurante2.Web;
                    Tipo_comida.Text = restaurante2.Tipo_comida.ToUpper();
                    Categoria.Text = restaurante2.Categoria.ToUpper();
                    Telefono = restaurante2.Telefono_uno;
                    Correo = restaurante2.Email;
                    Cabecera.Text = restaurante2.Poblaciones.ElementAt<Poblaciones>(0).Nombre;
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
   
        private void Button_Clicked(object sender, EventArgs e)
        {
            ICallService callService = DependencyService.Get<ICallService>();
            callService.MakeCall(Telefono);
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            IEmailService emailService = DependencyService.Get<IEmailService>();
            emailService.Send(Correo);
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

        private void BtnWeb_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PagWeb(Web));
        }
    }
}