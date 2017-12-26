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
    public partial class EmpresaTuristica : ContentPage
    {
        private string Slug { get; set; }
        private String Telefono { get; set; }
        private String Correo { get; set; }
        private String Web { get; set; }
        public EmpresaTuristica(string slug, string nombre)
        {
            InitializeComponent();
            BtnWeb.IsVisible = false;
            MetodoBoton();
            this.Slug = slug;
            Cabecera.Text = nombre;
        }

        public EmpresaTuristica(string slug)
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
                var emp = await obtenerResul.GetList<ClaseEmpresaTuristica>("http://descubrelavera.com/api/empresas_turisticas/detalle/" + Slug);
                ClaseUrl claseUrl = null;
                List<ClaseUrl> list_url = new List<ClaseUrl>();
                if (emp != null)
                {

                    foreach (ClaseEmpresaTuristica et in emp)
                    {
                        foreach (Imagenes i in et.Imagenes)
                        {
                            claseUrl = new ClaseUrl();
                            claseUrl.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + i.Url;
                            list_url.Add(claseUrl);
                        }
                        if (!et.Web.Equals("")) BtnWeb.IsVisible = true;
                        imagenes.Source = list_url.ElementAt<ClaseUrl>(0).Url;
                        nombre.Text = et.Nombre.ToUpper();
                        descripcion.Text = et.Descripcion_Corta;
                        Direccion.Text = et.Direccion.ToUpper();
                        Web = et.Web;
                        Tipo_empresa.Text = et.Tipo_Empresa.ToUpper();
                        Telefono = et.Telefono_uno;
                        Correo = et.Email;
                        Cabecera.Text = et.Poblaciones.ElementAtOrDefault<Poblaciones>(0).Nombre;
                        int cont = 0;
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
                    var emp2 = await obtenerResul.GetObject<ClaseEmpresaTuristica>("http://descubrelavera.com/api/empresas_turisticas/detalle/" + Slug);
                    foreach (Imagenes i in emp2.Imagenes)
                    {
                        claseUrl = new ClaseUrl();
                        claseUrl.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + i.Url;
                        list_url.Add(claseUrl);
                    }
                    if (!emp2.Web.Equals("")) BtnWeb.IsVisible = true;
                    imagenes.Source = list_url.ElementAt<ClaseUrl>(0).Url;
                    nombre.Text = emp2.Nombre.ToUpper();
                    descripcion.Text = emp2.Descripcion_Corta;
                    Direccion.Text = emp2.Direccion.ToUpper();
                    Web = emp2.Web;
                    Tipo_empresa.Text = emp2.Tipo_Empresa.ToUpper();
                    Telefono = emp2.Telefono_uno;
                    Correo = emp2.Email;
                    Cabecera.Text = emp2.Poblaciones.ElementAtOrDefault<Poblaciones>(0).Nombre;
                    int cont = 0;
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