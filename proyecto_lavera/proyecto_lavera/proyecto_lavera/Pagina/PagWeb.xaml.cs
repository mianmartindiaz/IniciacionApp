using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace proyecto_lavera.Pagina
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagWeb : ContentPage
    {
        
        public PagWeb(string url)
        {
            InitializeComponent();
            web.Source = url;
        }
    }
}