using Plugin.Geolocator;
using proyecto_lavera.BD;
using proyecto_lavera.Tabbed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace proyecto_lavera.Pagina
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Mapa: ContentPage
    {
        private string Slug_Poblacion { get; set; }
        private string Nombre { get; set; }
        public Mapa(string slug_Poblacion,string nombre)
        {
            InitializeComponent();
            MetodoBoton();
            this.Slug_Poblacion = slug_Poblacion;
            this.Nombre = nombre;
            Cabecera.Text = nombre;
           
        }
        protected override void OnAppearing()
        {
            //Creamos las variables Latitud,Longitud,direccion y localidad
            //y las inicializamos a 0 o vacías depende de la función que 
            //tengan en su futuro.
            var Latitud = 0.0;
            var Longitud = 0.0;
            var direccion = "";
            var localidad = "";

            Device.BeginInvokeOnMainThread(async () =>
            {
                //Creamos un objeto de la clase ObtenerResul
                //para sacar los datos de los locales y guardarlos
                //en las variables mediante un Json.
                ObtResult obtenerResul = new ObtResult();
                var servicio = await obtenerResul.GetList<ListServicioPoblacion>("http://descubrelavera.com/api/imagenes/poblaciones/"+Slug_Poblacion+"/principal/punto-interes");
                //creamos un foreach para insertar los datos
                if (servicio != null)
                {
                   
                    foreach (ListServicioPoblacion lsp in servicio)
                    {
                        //Añadimos a las variables los datos.
                        Latitud = lsp.Latitud;
                        Longitud = lsp.Longitud;
                        direccion = lsp.Direccion;
                        localidad = lsp.Poblacion.ElementAtOrDefault<BD.Poblaciones>(0).Nombre;
                        //Llamamos al método LlenarPins y le pasamos por parametro los datos.
                        LlenarPins(Latitud, Longitud, direccion, localidad,lsp.Nombre);
                    }

                    Locator(Latitud, Longitud);
                }
                else
                {
                   // Locator(Latitud, Longitud);
                    var  lsp = await obtenerResul.GetObject<ListServicioPoblacion>("http://descubrelavera.com/api/imagenes/poblaciones/" + Slug_Poblacion + "/principal/punto-interes");
                    Latitud = lsp.Latitud;
                    Longitud = lsp.Longitud;
                    direccion = lsp.Direccion;
                    localidad = lsp.Poblacion.ElementAtOrDefault<BD.Poblaciones>(0).Nombre;
                    //Llamamos al método LlenarPins y le pasamos por parametro los datos.
                    LlenarPins(Latitud, Longitud, direccion, localidad,lsp.Nombre);
                    await DisplayAlert("Información", "Servicios en mantenimiento", "Ok");
                    Navigation.RemovePage(this);
                }
            });
        }


        //El método LlenarPins es el encargado de hacer que cuando muestres el mapa
        //Salgan los globos con información sobre ese mapa y así poder saber cual de 
        //es la dirección etc... de los restaurantes que hay.
        public void LlenarPins(double latitud, double longitud, string direccion, string localidad,string nombre)
        {
            //creamos la variable position1 y le pasamos por parametro la latitud
            //y la longitud
            var position1 = new Position(latitud, longitud);
            //creamos un pin
            var pin1 = new Pin
            {
                //le decimos los datos que queremos que aparezcan
                //el tipo.
                //la posición que tiene que coger.
                //lo que va a mostrar cuando pinchemos en el.
                //la dirección del sitio.
                Type = PinType.Place,
                Position = position1,
                Label = nombre+", " + localidad,
                Address = direccion
            };
            //y este pin lo añadimos al mapa.
            MyMap.Pins.Add(pin1);
        }
        //El método Locator es el encargado de hacer la geolocalización
        //es decir, saber el punto exacto donde se encuantra la persona
        //con la aplicación en ese momento.



        public async void Locator(double latitud,double longitud)
        {
            //hacemos un try/catch para el control
            //de errores.
            try
            {
                //creamos una variable locator que contiene el geolocalizador.
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                //creamos la variable location que será igual nuestra posición.
                var location = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
                //creamos la variable position y le pasamos por parametro nuestra latitud y nuestra longitud
                var position = new Position(location.Latitude, location.Longitude);
                // y lo añadimos al mapa.
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(.3)));
            }
            catch (Exception e)
            {
                //si no tenemos la ubicación activada, nos dará una excepción, por lo que lo que hacemos es mostrar un restaurante
                //central , en estos momentos es el de Cáceres y mandamos un mensaje de Información para que inicien la ubicación.
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(latitud, longitud), Distance.FromKilometers(.1)));
                await DisplayAlert("Información", "Ha olvidado activar la ubicación", "Aceptar");
            }
        }
        protected void MetodoBoton()
        {
            var tapimage = new TapGestureRecognizer();
            tapimage.Tapped += TapimageCarta;
            menu.GestureRecognizers.Add(tapimage);

        }

        void TapimageCarta(Object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
            Navigation.PushModalAsync(new Principal());
        }
    }
}