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
    public partial class Transporte : ContentPage
    {
        private string Slug { get; set; }
        private String Telefono { get; set; }
        public Transporte(string slug, string nombre)
        {
            InitializeComponent();
            MetodoBoton();
            this.Slug = slug;
            Cabecera.Text = nombre;
        }

        public Transporte(string slug)
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
                var transporte = await obtenerResul.GetList<ClaseTransporte>("http://descubrelavera.com/api/transportes/detalle/" + Slug);
                List<ClaseUrl> list_url = new List<ClaseUrl>();
                if (transporte != null)
                {

                    foreach (ClaseTransporte t in transporte)
                    {
                        foreach (Imagenes i in t.Imagenes)
                        {
                            ClaseUrl claseUrl = new ClaseUrl();
                            claseUrl.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + i.Url;
                            list_url.Add(claseUrl);
                        }
                        imagenes.Source = list_url.ElementAt<ClaseUrl>(0).Url;
                        nombre.Text = t.Nombre.ToUpper();
                        descripcion.Text = t.Descripcion_Corta;
                        Direccion.Text = t.Direccion.ToUpper();
                        Telefono = t.Telefono_Uno;
                        Cabecera.Text = t.Poblaciones.ElementAtOrDefault<Poblaciones>(0).Nombre;
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
                    var trasporte2 = await obtenerResul.GetObject<ClaseTransporte>("http://descubrelavera.com/api/transportes/detalle/" + Slug);
                    foreach (Imagenes i in trasporte2.Imagenes)
                    {
                        ClaseUrl claseUrl = new ClaseUrl();
                        claseUrl.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + i.Url;
                        list_url.Add(claseUrl);
                    }
                    imagenes.Source = list_url.ElementAt<ClaseUrl>(0).Url;
                    nombre.Text = trasporte2.Nombre.ToUpper();
                    descripcion.Text = trasporte2.Descripcion_Corta;
                    Direccion.Text = trasporte2.Direccion.ToUpper();
                    Telefono = trasporte2.Telefono_Uno;
                    Cabecera.Text = trasporte2.Poblaciones.ElementAtOrDefault<Poblaciones>(0).Nombre;
                   
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