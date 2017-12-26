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
    public partial class Ruta : ContentPage
    {
        public string Slug { get; set; }
        public Ruta(string slug, string nombre)
        {
            InitializeComponent();
            MetodoBoton();
            this.Slug = slug;
            Cabecera.Text = nombre;
        }

        public Ruta(string slug)
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
                var ruta = await obtenerResul.GetList<ClaseRuta>("http://descubrelavera.com/api/rutas/detalle/" + Slug);

                List<ClaseUrl> list_url = new List<ClaseUrl>();
                if (ruta != null)
                {

                    foreach (ClaseRuta r in ruta)
                    {
                        foreach (Imagenes i in r.Imagenes)
                        {
                            ClaseUrl claseUrl = new ClaseUrl();
                            claseUrl.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + i.Url;
                            list_url.Add(claseUrl);
                        }
                        imagenes.Source = list_url.ElementAt<ClaseUrl>(0).Url;
                        nombre.Text = r.Nombre.ToUpper();
                        descripcion.Text = r.Descripcion_Corta;
                        Direccion.Text = r.Direccion.ToUpper();
                        Distancia.Text = r.Distancia.ToUpper();
                        Duracion.Text = r.Duracion.ToUpper();
                        Dificultad.Text = r.Dificultad.ToUpper();
                        Cabecera.Text = r.Poblaciones.ElementAtOrDefault<Poblaciones>(0).Nombre;
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

                        int num = 1;
                        foreach (Poblaciones p in r.Poblaciones)
                        {
                            Poblacion.Text += p.Nombre.ToUpper();
                            if (r.Poblaciones.Count > num)
                            {
                                Poblacion.Text += ",";
                            }

                        }

                       
                    }

                }
                else
                {
                    var ruta2 = await obtenerResul.GetObject<ClaseRuta>("http://descubrelavera.com/api/rutas/detalle/" + Slug);
                    foreach (Imagenes i in ruta2.Imagenes)
                    {
                        ClaseUrl claseUrl = new ClaseUrl();
                        claseUrl.Url = "http://descubrelavera.com/api/imagenes/mostrar/" + i.Url;
                        list_url.Add(claseUrl);
                    }
                    imagenes.Source = list_url.ElementAt<ClaseUrl>(0).Url;
                    nombre.Text = ruta2.Nombre.ToUpper();
                    descripcion.Text = ruta2.Descripcion_Corta;
                    Direccion.Text = ruta2.Direccion.ToUpper();
                    Distancia.Text = ruta2.Distancia.ToUpper();
                    Duracion.Text = ruta2.Duracion.ToUpper();
                    Dificultad.Text = ruta2.Dificultad.ToUpper();
                    Cabecera.Text = ruta2.Poblaciones.ElementAtOrDefault<Poblaciones>(0).Nombre;
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
                    int num = 1;
                    foreach (Poblaciones p in ruta2.Poblaciones)
                    {
                        Poblacion.Text += p.Nombre.ToUpper();
                        if (ruta2.Poblaciones.Count > num)
                        {
                            Poblacion.Text += ",";
                        }

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