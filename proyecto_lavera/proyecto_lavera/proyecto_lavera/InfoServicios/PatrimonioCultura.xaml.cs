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
    public partial class PatrimonioCultura : ContentPage
    {
        private string Slug { get; set; }
        private String Telefono { get; set; }
        private String Correo { get; set; }
        private String Web { get; set; }
        public PatrimonioCultura(string slug, string nombre)
        { 
            InitializeComponent();
            BtnEmail.IsVisible = false;
            BtnLlamar.IsVisible = false;
            BtnWeb.IsVisible = false;
            this.Slug = slug;
            MetodoBoton();
            Cabecera.Text = nombre;
        }

        public PatrimonioCultura(string slug)
        {
            InitializeComponent();
            BtnEmail.IsVisible = false;
            BtnLlamar.IsVisible = false;
            BtnWeb.IsVisible = false;
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
                var cultura = await obtenerResul.GetList<ClasePatrimonioCultura>("http://descubrelavera.com/api/patrimonios_cultura/detalle/" + Slug);

                List<ClaseUrl> list_url = new List<ClaseUrl>();
                if (cultura != null)
                {

                    foreach (ClasePatrimonioCultura pc in cultura)
                    {
                        foreach (Imagenes i in pc.Imagenes)
                        {
                            ClaseUrl claseUrl = new ClaseUrl();
                            claseUrl.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + i.Url;
                            list_url.Add(claseUrl);
                        }
                        if (pc.Visita)
                        {
                            BtnEmail.IsVisible = true;
                            BtnLlamar.IsVisible = true;
                            BtnWeb.IsVisible = true;
                        }
                        else
                        {
                            if (!pc.Telefono_Uno.Equals("") || !pc.Telefono_Dos.Equals("")) BtnLlamar.IsVisible = true;
                            if (!pc.Web.Equals("")) BtnWeb.IsVisible = true;
                            if (!pc.Email.Equals("")) BtnEmail.IsVisible = true;
                        }
                        imagenes.Source = list_url.ElementAt<ClaseUrl>(0).Url;
                        nombre.Text = pc.Nombre.ToUpper();
                        descripcion.Text = pc.Descripcion_Corta;
                        Direccion.Text = pc.Direccion.ToUpper();
                        Web = pc.Web;
                        Horario.Text = pc.Horario.ToUpper();
                        Precio.Text = Convert.ToString(pc.Precio);
                        Telefono = pc.Telefono_Uno;
                        Correo = pc.Email;
                        Cabecera.Text = pc.Poblaciones.ElementAtOrDefault<Poblaciones>(0).Nombre;
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
                    var cultura2 = await obtenerResul.GetObject<ClasePatrimonioCultura>("http://descubrelavera.com/api/patrimonios_cultura/detalle/" + Slug);
                    foreach (Imagenes i in cultura2.Imagenes)
                    {
                        ClaseUrl claseUrl = new ClaseUrl();
                        claseUrl.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + i.Url;
                        list_url.Add(claseUrl);
                    }
                    if (!cultura2.Visita)
                    {
                        BtnEmail.IsVisible = false;
                        BtnLlamar.IsVisible = false;
                        BtnWeb.IsVisible = false;
                    }
                    else 
                    {
                        if (cultura2.Telefono_Uno.Equals("") || cultura2.Telefono_Dos.Equals(""))BtnLlamar.IsVisible = false;
                        if (cultura2.Web.Equals("")) BtnWeb.IsVisible = false;
                        if (cultura2.Email.Equals("")) BtnEmail.IsVisible = false;
                    }
                    
                    imagenes.Source = list_url.ElementAt<ClaseUrl>(0).Url;
                    nombre.Text = cultura2.Nombre.ToUpper();
                    descripcion.Text = cultura2.Descripcion_Corta;
                    Direccion.Text = cultura2.Direccion.ToUpper();
                    Web = cultura2.Web;
                    Horario.Text = cultura2.Horario;
                    Precio.Text = Convert.ToString(cultura2.Precio);
                    Telefono = cultura2.Telefono_Uno;
                    Correo = cultura2.Email;
                    Cabecera.Text = cultura2.Poblaciones.ElementAtOrDefault<Poblaciones>(0).Nombre;
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