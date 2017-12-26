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
    public partial class ActividadOcio : ContentPage
    {
      
        private string Slug { get; set; }
        private String Telefono { get; set; }
        private String Correo { get; set; }
        private String Web { get; set; }
        public ActividadOcio(string slug, string nombre)
        {
            InitializeComponent();
            MetodoBoton();
            this.Slug = slug;
            Cabecera.Text = nombre;
           
        }
        public ActividadOcio(string slug)
        {
            InitializeComponent();
            this.Slug = slug;
          
        }


        protected override void OnAppearing()
        {
            CargarDetalle();
          
        }

    

        private void CargarDetalle()
        {
           
            Device.BeginInvokeOnMainThread(async () =>
            {
                ObtResult obtenerResul = new ObtResult();
                var actividad_ocio = await obtenerResul.GetList<ClaseActividadOcio>("http://descubrelavera.com/api/actividades_ocio/detalle/" + Slug);
                var actividadOcio2 = await obtenerResul.GetObject<ClaseActividadOcio>("http://descubrelavera.com/api/actividades_ocio/detalle/" + Slug);
                List<ClaseUrl> list_url = new List<ClaseUrl>();
                if (actividad_ocio != null)
                {


                    foreach (ClaseActividadOcio ao in actividad_ocio)
                    {
                        if (ao.Imagenes != null)
                        {
                            foreach (Imagenes i in ao.Imagenes)
                            {
                                ClaseUrl claseUrl = new ClaseUrl();
                                claseUrl.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + i.Url;
                                list_url.Add(claseUrl);
                            }
                            imagenes.Source = list_url.ElementAt<ClaseUrl>(0).Url;
                        }
                        else
                        
                        await DisplayAlert("", "Error al cargar las imagenes", "OK");
                        nombre.Text = ao.Nombre.ToUpper();
                        descripcion.Text = ao.Descripcion_Corta;
                        Direccion.Text = ao.Direccion.ToUpper();
                        Tipo_Actividad.Text = ao.Tipo_Actividad.ToUpper();
                        Telefono = ao.Telefono_uno;
                        Correo = ao.Email;
                        Web = ao.Web;
                        Cabecera.Text = ao.Poblaciones.ElementAtOrDefault<Poblaciones>(0).Nombre;
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
                else if (actividadOcio2 != null)
                {

                    if (actividadOcio2.Imagenes != null)
                    {
                        foreach (Imagenes i in actividadOcio2.Imagenes)
                        {
                            ClaseUrl claseUrl = new ClaseUrl();
                            claseUrl.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + i.Url;
                            list_url.Add(claseUrl);
                        }
                        imagenes.Source = list_url.ElementAt<ClaseUrl>(0).Url;
                    }
                    else
                    
                        await DisplayAlert("", "Error al cargar las imagenes", "OK");
                        nombre.Text = actividadOcio2.Nombre.ToUpper();
                        descripcion.Text = actividadOcio2.Descripcion_Corta;
                        Direccion.Text = actividadOcio2.Direccion.ToUpper();
                        Tipo_Actividad.Text = actividadOcio2.Tipo_Actividad.ToUpper();
                        Telefono = actividadOcio2.Telefono_uno;
                        Correo = actividadOcio2.Email;
                        Web = actividadOcio2.Web;
                        Cabecera.Text = actividadOcio2.Poblaciones.ElementAtOrDefault<Poblaciones>(0).Nombre;
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
            Navigation.PushModalAsync(new Comentarios(Slug,Cabecera.Text));
        }

        private void BtnWeb_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PagWeb(Web));
        }
    }
}