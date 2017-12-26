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
    public partial class Alojamiento : ContentPage
    {
        private string Slug { get; set; }
        private String Telefono { get; set; }
        private String Correo { get; set; }
        private String Web { get; set; }
        public Alojamiento(string slug, string nombre)
        {
            InitializeComponent();
            MetodoBoton();
            this.Slug = slug;
            Cabecera.Text = nombre;
        }

        public Alojamiento(string slug)
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
                var alojamiento = await obtenerResul.GetList<ClaseAlojamiento>("http://descubrelavera.com/api/alojamientos/detalle/" + Slug);
                var alojamiento2 = await obtenerResul.GetObject<ClaseAlojamiento>("http://descubrelavera.com/api/alojamientos/detalle/" + Slug);
                List<ClaseUrl> list_url = new List<ClaseUrl>();
                if (alojamiento != null)
                {

                    foreach (ClaseAlojamiento al in alojamiento)
                    {
                        if (al.Imagenes != null)
                        {
                            foreach (Imagenes i in al.Imagenes)
                            {
                                ClaseUrl claseUrl = new ClaseUrl();
                                claseUrl.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + i.Url;
                                list_url.Add(claseUrl);
                            }
                        }
                        else await DisplayAlert("", "Error al cargar las imagenes", "OK");
                        nombre.Text = al.Nombre.ToUpper();
                        descripcion.Text = al.Descripcion_Corta;
                        Direccion.Text = al.Direccion.ToUpper();
                        Web = al.Web;
                        Categoria.Text = al.Categoria.ToUpper();
                        Tipo_Alojamiento.Text = al.Tipo_Alojamiento.ToUpper();
                        Telefono = al.Telefono_uno;
                        Cabecera.Text = al.Poblaciones.ElementAtOrDefault<Poblaciones>(0).Nombre;
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
                else if (alojamiento2 != null)
                {
                    if (alojamiento2.Imagenes != null)
                    {
                        foreach (Imagenes i in alojamiento2.Imagenes)
                        {
                            ClaseUrl claseUrl = new ClaseUrl();
                            claseUrl.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + i.Url;
                            list_url.Add(claseUrl);
                        }
                    }
                    else await DisplayAlert("", "Error al cargar las imagenes", "OK");
                   
                    imagenes.Source = list_url.ElementAt<ClaseUrl>(0).Url;
                    nombre.Text = alojamiento2.Nombre.ToUpper();
                    descripcion.Text = alojamiento2.Descripcion_Corta;
                    Direccion.Text = alojamiento2.Direccion.ToUpper();
                    Web = alojamiento2.Web;
                    Categoria.Text = alojamiento2.Categoria.ToUpper();
                    Tipo_Alojamiento.Text = alojamiento2.Tipo_Alojamiento.ToUpper();
                    Telefono = alojamiento2.Telefono_uno;
                    Correo = alojamiento2.Email;
                    Cabecera.Text = alojamiento2.Poblaciones.ElementAtOrDefault<Poblaciones>(0).Nombre;
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
                else await DisplayAlert("", "Error de carga Json", "OK");

            });
        }
   

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (Telefono!=null){
                ICallService callService = DependencyService.Get<ICallService>();
                callService.MakeCall(Telefono);
            }
            
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