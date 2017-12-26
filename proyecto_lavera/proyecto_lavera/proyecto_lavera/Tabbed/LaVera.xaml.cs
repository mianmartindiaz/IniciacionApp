using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using proyecto_lavera.Service;
using proyecto_lavera.BD;

namespace proyecto_lavera.Tabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LaVera : ContentPage
    {
       
        public LaVera()
        {
            InitializeComponent();
        }

       

        protected override void OnAppearing()
        {
            base.OnAppearing();


            var template = new DataTemplate(typeof(DefaultTemplate));
            var view = new AccordionView(template);
            List<Section> ListaCategoria = new List<Section>();
            view.SetBinding(AccordionView.ItemsSourceProperty, "List");
            view.Template.SetBinding(AccordionSectionView.TitleProperty, "Title");
            view.Template.SetBinding(AccordionSectionView.ItemsSourceProperty, "List");
            Device.BeginInvokeOnMainThread(async () =>
            {
                ObtResult obtenerResul = new ObtResult();
                var lavera = await obtenerResul.GetList<ClaseLaVera>("http://descubrelavera.com/api/historias/lista/");

                foreach (ClaseLaVera lv in lavera)
                {
                    lv.Imagen_Movil = "http://descubrelavera.com/api/imagenes/mostrar/" + lv.Imagen_Movil;
                    ListaCategoria.Add(new Section
                    {
                        Title = lv.Imagen_Movil,
                        List = new List<ShoppingCart> {
                            new ShoppingCart {Descripcion=lv.Descripcion_Corta},
                        }
                    });
                }
                view.BindingContext = new ViewModel
                {
                    List = ListaCategoria
                };

                this.Content = view;
            });
        }
    }
    public class ShoppingCart
    {
       
        public string Descripcion { get; set; }
    }

    public class Section
    {
        public string Title { get; set; }
        public IEnumerable<ShoppingCart> List { get; set; }
    }

    public class ViewModel
    {

        public IEnumerable<Section> List { get; set; }
    }
}